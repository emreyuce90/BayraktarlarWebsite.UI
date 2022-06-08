using BayraktarlarWebsite.BLL.Concrete;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
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
            services.AddDbContext<DatabaseConnection>();
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
            
            services.AddScoped<ITabelaService, TabelaManager>();
            return services;
        }
    }
}
