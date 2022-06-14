using BayraktarlarWebsite.BLL.Extensions.MicrosoftIoC;
using BayraktarlarWebsite.BLL.Mappings;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Helpers.Abstract;
using BayraktarlarWebsite.UI.Helpers.Concrete;
using BayraktarlarWebsite.UI.Mappings.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
       
      private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDependencies(_configuration.GetConnectionString("db1"));
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyToastr();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddAutoMapper(typeof(BrandMap),typeof(CustomerMap),typeof(TabelaImagesMap),typeof(TabelaViewModelMap),typeof(MaterialMap));
            services.AddSession();
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
                app.UseEndpoints(builder => builder.MapDefaultControllerRoute());
            }
        }
    }
