using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class TicketUpdateDto
    {
        public int Id { get; set; }
        //Konu
        [Required(ErrorMessage = "Lütfen bir konu belirtiniz")]
        [DisplayName("Konu")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Lütfen  ait açıklamasını yazınız")]
        [DisplayName("Açıklama")]
        //Detay
        public string Detail { get; set; }
        public int UrgencyId { get; set; }
        //navigation propertysi
        public Urgency Urgency { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime RemainderDate { get; set; }
    }
}
