using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class EmailSendDto
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string UserEmailAddress { get; set; }
    }
}
