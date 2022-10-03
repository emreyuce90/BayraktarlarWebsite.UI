using BayraktarlarWebsite.Shared.Interface;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Hedef:IEntity
    {
        public int Id { get; set; }

        public int AltMarkaId { get; set; }
        public AltMarka AltMarka { get; set; }
        //sertan BAHADIR
        public int UserId { get; set; }
        public User User { get; set; }
        //Yıllık hedef
        public int TargetPerYear { get; set; }
        //Aylık hedef
        public int TargetPerMonth { get; set; }
    }
}
