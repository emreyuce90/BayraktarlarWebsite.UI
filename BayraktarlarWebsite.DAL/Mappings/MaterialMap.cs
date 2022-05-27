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
    public class MaterialMap : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Name).HasMaxLength(50);
            builder.Property(m => m.Detail).HasMaxLength(300);
            builder.HasData(
                new Material
                {
                    Id=1,
                    Name = "Vinil Germe(Standart Tabela)"
                }, new Material
                {
                    Id = 2,
                    Name = "Vinil Mesh(Delikli Bez Tabela)"
                }, new Material
                {
                    Id = 3,
                    Name = "One Way Visione (Camlara Uygulama)"
                }, new Material
                {
                    Id = 4,
                    Name = "Branda"
                }, new Material
                {
                    Id = 5,
                    Name = "Delikli Branda*"
                }, new Material
                {
                    Id = 6,
                    Name = "Diğer"

                });
        }
    }
}
