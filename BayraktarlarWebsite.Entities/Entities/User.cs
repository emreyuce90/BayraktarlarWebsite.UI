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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EntryDate { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Profile { get; set; }
        public List<Tabela> Tabelas { get; set; }
        //Bir kullanıcının birden fazla satışı olabilir
        public IList<Sellout> Sellouts { get; set; }
        //Bir kullanıcının birden fazla hedefi olabilir
        public IList<Hedef> Hedefler { get; set; }
        //Bir kullanıcının birden fazla tahsilatı olabilir
        public IList<Tahsilat> Tahsilatlar { get; set; }
        //Bir kullanıcının birden fazla cirosu olabilir
        public IList<Ciro> Cirolar { get; set; }
    }
}
