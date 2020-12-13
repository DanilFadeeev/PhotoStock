using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhothoStock.Data.DTO;
using PhothoStock.Models.Identity;
using PhothoStock.Utils;
using System.IO;

namespace PhothoStock.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        //adminpass!
        public IActionResult Login(string returnurl)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect(returnurl is null ? "/" : returnurl);
            ViewData["returnurl"] = returnurl;
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginData data, string returnurl)
        {
            var user = UserManager.FindByEmailAsync(data.Email).Result;
            if (ModelState.IsValid && user is not null && user.IsEnabled)
            {
                if (user.PasswordHash == data.Password)
                    SignInManager.SignInAsync(user, true).Wait();
                else return RedirectToAction(nameof(Login));
                return Redirect(returnurl is null? "/":returnurl);
            }
            return RedirectToAction("login");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]//password11!
        public IActionResult SignUp(SignUpData data, [FromServices] IWebHostEnvironment env)
        {
            var EmailIsFree = UserManager.FindByEmailAsync(data.Email).Result is null;
            var NameIsFree = UserManager.FindByNameAsync(data.Nickname).Result is null;
            IdentityResult a;
            if (EmailIsFree && NameIsFree)
            {
                a = UserManager.CreateAsync(data.ToApplicationUser()).Result;
                if (a.Succeeded)
                {
                    string userFolder = env.WebRootPath + $"/UsersPhotos/{data.Nickname}/";
                    Directory.CreateDirectory(userFolder);
                }
                return Redirect("/");
            }
            return View();
        }
        [Authorize]
        public IActionResult LogOut()
        {
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            SignInManager.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}
