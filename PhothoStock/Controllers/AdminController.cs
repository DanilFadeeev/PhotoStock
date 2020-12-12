using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using PhothoStock.Data;
using PhothoStock.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        public ApplicationDbContext Context { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            Context = context;
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListOfUsers()
        {
            var users = Context.Users.Where(u=>u.UserName!=User.Identity.Name).ToList();
            return View(users);
        }
        public IActionResult Delete(string UserLogin) 
        {
            var user = UserManager.FindByNameAsync(UserLogin).Result;
            var deleteResult = UserManager.DeleteAsync(user).Result;
            return RedirectToAction(nameof(ListOfUsers)); 
        }
        public IActionResult Block(string UserLogin)
        {
            var user = UserManager.FindByNameAsync(UserLogin).Result;
            user.LockoutEnd = new(DateTime.Now.AddYears(100));
            user.LockoutEnabled = true;
            Context.SaveChanges();
            return RedirectToAction(nameof(ListOfUsers));
        }
        public IActionResult Unblock(string UserLogin)
        {
            var user = UserManager.FindByNameAsync(UserLogin).Result;
            user.LockoutEnd = null;
            Context.SaveChanges();
            return RedirectToAction(nameof(ListOfUsers));
        }
        public IActionResult Info(string UserLogin)
        {
            var user = Context.Users.Where(u=>u.UserName ==UserLogin).FirstOrDefault();
            var userPhotos = Context.Photos.Where(p=>p.UserName == UserLogin).ToList();
            user.Photos = userPhotos;
            return View(user);
        }
        public IActionResult Categories()
        {
            var categories = Context.Filter.DistinctBy(f=>f.Category).Select(f=>f.Category).ToList();
            return View(categories);
        }
        public IActionResult DeleteCategory(string categoryName)
        {
            var delete = Context.Filter.Where(f => f.Category == categoryName).ToList();
            Context.Filter.RemoveRange(delete);
            Context.SaveChanges();
            return RedirectToAction(nameof(Categories));
        }
        public IActionResult AddCategory(string categoryName)
        {
            Context.Filter.Add(new() { Category = categoryName });
            Context.SaveChanges();
            return RedirectToAction(nameof(Categories));
        }
        public IActionResult DeletePhoto(int id)
        {
            var photo = Context.Photos.Where(p => p.Id == id).Include(p=>p.Categories).First();
            Context.Remove(photo);
            Context.SaveChanges();
            return RedirectToAction(nameof(Categories));
        }
    }
}
