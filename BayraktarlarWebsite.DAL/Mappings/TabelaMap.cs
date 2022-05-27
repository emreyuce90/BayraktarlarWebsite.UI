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
    public class TabelaMap : IEntityTypeConfiguration<Tabela>
    {
        public void Configure(EntityTypeBuilder<Tabela> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.BrandId).IsRequired();
            builder.Property(t => t.StatusId).IsRequired();
            builder.Property(t => t.MaterialId).IsRequired();
            builder.Property(t => t.CustomerId).IsRequired();
            builder.Property(t => t.UserId).IsRequired();

        }
    }
}
