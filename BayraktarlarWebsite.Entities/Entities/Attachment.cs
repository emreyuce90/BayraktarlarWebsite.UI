using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Attachment : IEntity
    {
        public int Id { get; set; }
        public string AttachmentUrl { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
