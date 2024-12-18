﻿using BayraktarlarWebsite.BLL.Concrete;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore;
using BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Extensions.MicrosoftIoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,string connectionString)
        {
            //opt=>opt.UseSqlServer("server=localhost;database=bayraktarlarDb;integrated security =true"
            services.AddDbContext<DatabaseConnection>(opt => opt.UseSqlServer(connectionString));
            services.AddIdentity<User, Role>(opt =>
            {
                //User settings
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
                //Password Settings
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredUniqueChars = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 1;
            }

           ).AddEntityFrameworkStores<DatabaseConnection>().AddDefaultTokenProviders();
            services.AddScoped<IHunterService,HunterManager>();
            services.AddScoped<IStatusService, StatusManager>();
            services.AddScoped<IMaterialService, MaterialManager>();
            services.AddScoped<ITabelaService, TabelaManager>();
            services.AddScoped<ITabelaImagesService, TabelaImagesManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILetService, LetManager>();
            services.AddScoped<INotificationService, NotificationManager>();
            services.AddScoped<ITicketService, TicketManager>();
            services.AddScoped<IUrgencyService, UrgencyManager>();
            services.AddSingleton<IMailService, MailManager>();
            services.AddScoped<ICiroService, CiroManager>();
            services.AddScoped<ITahsilatService, TahsilatManager>();
            services.AddScoped<ISellService, SellManager>();
            services.AddScoped<IHedefService, HedefManager>();
            services.AddScoped<ITownService, TownManager>();
            services.AddScoped<IDistrictService, DistrictManager>();
            return services;
        }
    }
}
