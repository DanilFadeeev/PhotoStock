using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhothoStock.Data.DTO;
using PhothoStock.Models.Identity;
using PhothoStock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhothoStock.Data;
using PhothoStock.Models;
using MoreLinq;
using Microsoft.EntityFrameworkCore;

namespace PhothoStock.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public ApplicationDbContext Context { get; }

        public HomeController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> rolemanager,
            ApplicationDbContext context)
        {
            UserManager = userManager;
            RoleManager = rolemanager;
            Context = context;
        }

        public IActionResult Index()
        {
            var filters = Context.Filter
                .DistinctBy(f => f.Category)
                .Select(f => new Filter() { Active = true, Category = f.Category })
                .ToList();
            ViewData.Model = filters;
            return View();
        }
        public IActionResult Secret()
        {
            return View(); 
        }
        [Authorize(Roles ="admin")]
        public IActionResult AdminOnly()
        {
            return View();
        }
        public IActionResult ShowData(int data)
        {
            var c = HttpContext.Request.Query.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult test() { return null; }
        [HttpPost]
        public IActionResult Test() { return null; }
        private List<Filter> generateFilters()
        {
            List<Filter> result = new()
            {
                new() { Category = "Girls"},
                new() { Category = "Animals"},
                new() { Category = "Art"},
                new() { Category = "Nature"},
                new() { Category = "Cars"},
                new() { Category = "Sport"},
                new() { Category = "Anime"}
            };
            return result;
        }
    }
}
