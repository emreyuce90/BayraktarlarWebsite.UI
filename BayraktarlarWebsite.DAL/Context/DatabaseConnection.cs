using BayraktarlarWebsite.DAL.Mappings;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Context
{
    public class DatabaseConnection:IdentityDbContext<User,Role,int>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tabela> Tabelas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: "server=localhost;database=bayraktarlarDb;integrated security =true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BrandMap());
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new MaterialMap());
            builder.ApplyConfiguration(new StatusMap());
            builder.ApplyConfiguration(new TabelaMap());
            base.OnModelCreating(builder);
        }
    }
}
