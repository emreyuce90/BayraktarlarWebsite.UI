using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Helpers.Abstract;
using BayraktarlarWebsite.UI.Helpers.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BayraktarlarWebsite.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Runtime servisi
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyToastr();
            //Veritabaný baðlantý servisi
            services.AddDbContext<DatabaseConnection>();

            //imagehelper servisi
            services.AddScoped<IImageHelper, ImageHelper>();
            //Identity servisi
            services.AddIdentity<User, Role>(opt =>
            {
                //User settings
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
                //Password Settings
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredUniqueChars = 1;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 5;
            }

           ).AddEntityFrameworkStores<DatabaseConnection>();
            //cookie servisi
            services.ConfigureApplicationCookie(opt =>
            {
                opt.AccessDeniedPath = new PathString("/Admin/Account/AccessDenied");
                opt.LoginPath = new PathString("/Admin/Account/Login");
                opt.LogoutPath = new PathString("/Admin/Account/Logout");
                opt.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "BayraktarlarWebSite",
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, //Always olmalý

                };
                //Kullanýcý cookie oluþturduktan sonra 7 gün içerisinde tekrar giriþ yaparsa +7 gün daha uzatýr
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(7);
            });
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();
            app.UseEndpoints(builder =>builder.MapDefaultControllerRoute());
        }
    }
}
