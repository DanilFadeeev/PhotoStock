using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhothoStock.Data;
using PhothoStock.Data.DTO;
using PhothoStock.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using PhothoStock.Utils;

namespace PhothoStock.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private string userFolder { get; set; }
        public ApplicationDbContext Context { get; }
        public PhotoController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            Context = context;
            userFolder = env.WebRootPath + $"\\UsersPhotos\\{User?.Identity?.Name}";
        }


        [Authorize]
        public IActionResult AddPhoto()
        {
            PhotoPostData data = new() { Filters = Context.Filter.DistinctBy(f => f.Category).ToList() };
            ViewData["UserName"] = User.Identity.Name;
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostPhoto(PhotoPostData post, [FromServices] IWebHostEnvironment env)
        {
            string fileName = DateTime.Now.ToString("yymmssffff");
            string extension = Path.GetExtension(post.Photo.FileName);

            string fullPath = env.WebRootPath + $"\\UsersPhotos\\{User?.Identity?.Name}\\" + fileName + extension;

            using (FileStream fs = new(fullPath, FileMode.Create))
                post.Photo.CopyToAsync(fs).Wait();
            Context.Photos.Add(
                new()
                {
                    UserName = User.Identity.Name,
                    PhotoName = fileName + extension,
                    Categories = post.Filters
                }
            );
            Context.SaveChanges();
            return RedirectToAction(nameof(ShowAllUserPhotos));
        }

        [Authorize]
        public IActionResult ShowAllUserPhotos([FromServices] IWebHostEnvironment env)
        {
            userFolder = env.WebRootPath + $"\\UsersPhotos\\{User?.Identity?.Name}";
            var photosNames = Context.Photos.Where(p => p.UserName == User.Identity.Name).Select(p => p.PhotoName);
            ViewData["UserName"] = User.Identity.Name;

            return View(photosNames);
        }
        public IActionResult ShowAllPhotos(List<Filter> filters = null, int page = 0)
        {
            var photosNames = FiltredPhotos(filters);
            ViewData["page"] = page;
            ViewData["totalPages"] = 100;

            PhotoInfoAndFilters photosnandphilters = new()
            {
                PhotosInfo = photosNames,
                Filters = filters.Count > 0 ? filters : allFilters
            };
            return View(photosnandphilters);
        }
        public IActionResult ShowPhotoInfo(int photoId)
        {
            var c = Context.Photos
                .Where(ph => ph.Id == photoId)
                .Include(ph => ph.Categories)
                .FirstOrDefault();
            return View(c);
        }
        private List<PhotoInfo> FiltredPhotos(List<Filter> filters)
        {
            if (filters is null || filters.Count == 0 || filters.NoFiltersSet())
                return Context.Photos
                    .DistinctBy(p => p.PhotoName)
                    .ToList();

            return Context.Photos
                .Include(p => p.Categories)
                .ToList()
                .Where(IsInFilters)
                .ToList();

            bool IsInFilters(PhotoInfo photoInfo)
            {
                foreach (var f in photoInfo?.Categories ?? new List<Filter>())
                    foreach (var fil in filters)
                        if (f.Active && fil.Active && f.Category == fil.Category)
                            return true;
                return false;
            }
        }
        private List<Filter> allFilters => Context.Filter.DistinctBy(f => f.Category).ToList();
    }
}
