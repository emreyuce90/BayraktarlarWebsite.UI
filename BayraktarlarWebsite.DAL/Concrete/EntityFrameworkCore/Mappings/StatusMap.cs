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
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(s=>s.Id);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(25);
            builder.HasData(
                new Status
                {
                    Id=1,
                    Name="Onayda"

                },
                new Status
                {
                    Id = 2,
                    Name = "Onaylandı"
                },
                new Status
                {
                    Id = 3,
                    Name = "Tasarımda"
                },
                new Status
                {
                    Id = 4,
                    Name = "Üretimde"
                },new Status
                {
                    Id = 5,
                    Name = "Uygulandı"
                }
                );
        }
    }
}
