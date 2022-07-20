using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class User: IdentityUser<int>
    {
        public DateTime EntryDate { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Profile { get; set; }
        public List<Tabela> Tabelas { get; set; }

    }
}
