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
    public class TicketAddDto
    {
        
        //Konu
        [Required(ErrorMessage = "Lütfen bir konu belirtiniz")]
        [DisplayName("Konu")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Lütfen  ait açıklamasını yazınız")]
        [DisplayName("Açıklama")]
        //Detay
        public string Detail { get; set; }
        //Ekler
        public List<Attachment> Attachments { get; set; }
        //Kapatıldı mı
        public bool IsClosed { get; set; }
        //Bilet açılış tarihi
        public DateTime CreatedDate { get; set; }
        //Bilet kapanış tarihi
        public DateTime ClosedDate { get; set; }
        //Aciliyet Id si
        public int UrgencyId { get; set; }
        //navigation propertysi
        public Urgency Urgency { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime RemainderDate { get; set; }
        public bool IsAssigned { get; set; }

    }
}
