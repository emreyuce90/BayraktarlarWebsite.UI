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
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            //marka
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Name).HasMaxLength(25);

            //db kurulurken markaların eklenmesi
            builder.HasData(
                new Brand
                {
                    Id=1,
                    Name = "Mobil PVL (Mobil 1)"
                }, new Brand
                {
                    Id = 2,
                    Name = "Mobil CVL (Mobil Delvac)"

                }, new Brand
                {
                    Id = 3,
                    Name = "Mobil 1 Center"
                }, new Brand
                {
                    Id = 4,
                    Name = "Petronas Syntium"
                }, new Brand
                {
                    Id = 5,
                    Name = "Petronas Selenia"
                }, new Brand
                {
                    Id = 6,
                    Name = "Petronas Urania"
                }, new Brand
                {
                    Id = 7,
                    Name = "Petronas Ambra"
                }, new Brand
                {
                    Id = 8,
                    Name = "Diğer"
                }); ;

        }
    }
}
