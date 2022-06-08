using BayraktarlarWebsite.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            //name
            builder.Property(c =>c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(300);
            //code
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.Code).HasMaxLength(5);
            //address
            builder.Property(c => c.Address).IsRequired();
            builder.Property(c => c.Address).HasMaxLength(500);
            //city
            builder.Property(c => c.City).IsRequired();
            builder.Property(c => c.City).HasMaxLength(50);
            //town
            builder.Property(c => c.Town).IsRequired();
            builder.Property(c => c.Town).HasMaxLength(50);
            //email
            builder.Property(c => c.EMail).IsRequired();
            builder.Property(c => c.EMail).HasMaxLength(50);

        }
    }
}
