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
        public DbSet<Let> Lets { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tabela> Tabelas { get; set; }
        public DbSet<TabelaImages> TabelaImages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Ciro> Cirolar { get; set; }
        public DbSet<Tahsilat> Tahsilatlar { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<AltMarka> AltMarkalar { get; set; }
        public DbSet<Hedef> Hedefler { get; set; }
        public DbSet<Sellout> Satislar { get; set; }
        public DatabaseConnection(DbContextOptions<DatabaseConnection> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BrandMap());
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new MaterialMap());
            builder.ApplyConfiguration(new StatusMap());
            builder.ApplyConfiguration(new TabelaMap());
            builder.Entity<Tabela>()
       .HasMany(b => b.Images)
       .WithOne(a => a.Tabela)
       .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
    }
}
