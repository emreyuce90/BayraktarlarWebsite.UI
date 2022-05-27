using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Müşterinin cari kodu burada tutulacak
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Mobile { get; set; }
        public string EMail { get; set; }

    }
}
