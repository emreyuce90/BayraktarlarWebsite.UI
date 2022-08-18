using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class User: IdentityUser<int>
    {
        [Required(ErrorMessage ="İsim alanı boş geçilemez")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]

        public string LastName { get; set; }
        public DateTime EntryDate { get; set; }
        [Required(ErrorMessage = "Kod alanı boş geçilemez")]

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
