using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class LetAddDto
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        //Bitiş Tarihi
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(2);
        //Not
        public string Note { get; set; }
        //Kim kullanacak ?
        [Required]
        public int UserId { get; set; }
        //İzin onaylandı mı?
        public bool IsApproved { get; set; } = false;
        //İznin onaylandığı tarih
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Range(1, Double.PositiveInfinity,ErrorMessage ="Girilen değer 1'den az olamaz")]
        public int DayCount { get; set; }
    }
}
