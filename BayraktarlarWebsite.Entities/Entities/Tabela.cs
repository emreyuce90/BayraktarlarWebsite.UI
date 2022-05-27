using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Tabela
    {
        public int Id { get; set; }
        public string Pictures { get; set; }

        public string Notes { get; set; }
        //Müşteri 
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        //Marka
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        //Tabela Malzemesi
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        //Oluşturan Kullanıcı
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        //Aktif mi?
        public bool IsActive { get; set; } = true;
        //Silinmiş mi?
        public bool IsDeleted { get; set; }
        //Tabelanın durumu nedir? 
        public int StatusId { get; set; }
        public Status Status { get; set; }


    }
}
