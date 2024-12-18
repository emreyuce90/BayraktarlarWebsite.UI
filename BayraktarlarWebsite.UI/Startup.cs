using BayraktarlarWebsite.BLL.Extensions.MicrosoftIoC;
using BayraktarlarWebsite.BLL.Mappings;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Helpers.Abstract;
using BayraktarlarWebsite.UI.Helpers.Concrete;
using BayraktarlarWebsite.UI.Mappings.AutoMapper;
using BayraktarlarWebsite.UI.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Shared.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            services.Configure<SmtpSettings>(_configuration.GetSection("SmtpSettings"));
            services.Configure<SeoInfo>(_configuration.GetSection("SeoInfo"));
            services.ConfigureWritable<SeoInfo>(_configuration.GetSection("SeoInfo"));
            services.AddDependencies(_configuration.GetConnectionString("db2"));
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyToastr().AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<ILetTimeCalculator, LetTimeCalculator>();
            services.AddAutoMapper(typeof(BrandMap),typeof(CustomerMap),typeof(TabelaImagesMap),typeof(TabelaViewModelMap),typeof(MaterialMap),typeof(TabelaMap),typeof(RolesMapping),typeof(TicketMap));
            services.AddSession();
            //cookie servisi
            services.ConfigureApplicationCookie(opt =>
            {
                opt.AccessDeniedPath = new PathString("/Users/AccessDenied");
                opt.LoginPath = new PathString("/Auth/Login");
                opt.LogoutPath = new PathString("/Auth/Logout");
                opt.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "BayraktarlarWebSite",
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, //Always olmal�

                };
                //Kullan�c� cookie olu�turduktan sonra 7 g�n i�erisinde tekrar giri� yaparsa +7 g�n daha uzat�r
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
