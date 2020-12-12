using ImageStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStore
{
    public class Startup
    {
        IConfiguration Cfg;
        public Startup(IConfiguration cfg)
        {
            Cfg = cfg;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ImageDbContext>(
                options => options.UseSqlServer(Cfg.GetConnectionString("default")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(errorApp => errorApp.Run(new ExceptionHandler().Handle));
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
    class ExceptionHandler
    {
        public async Task Handle(HttpContext context)
        {
            if(context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
               await context.Response.WriteAsync("404 Error");
            }
            await context.Response.WriteAsync("custom handler");
        }
    }
}
