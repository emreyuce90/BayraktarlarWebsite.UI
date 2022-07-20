using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Let:IEntity
    {
        public int Id { get; set; }
        //Başlangıç tarihi
        [Required]
        public DateTime StartDate { get; set; }
        //Bitiş Tarihi
        [Required]
        public DateTime EndDate { get; set; }
        //Not
        public string Note { get; set; }
        //Kim kullanacak ?
        [Required]
        public int UserId { get; set; }
        //İzin onaylandı mı?
        public User User { get; set; }
        public bool IsApproved { get; set; } = false;
        //İznin onaylandığı tarih
        [Required]
        public DateTime CreatedDate { get; set; }

        public int DayCount { get; set; }

    }
}
