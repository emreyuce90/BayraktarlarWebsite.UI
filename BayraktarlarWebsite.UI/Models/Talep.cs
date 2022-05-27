using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class Talep
    {
        public List<CustomerViewModel> CustomerViewModel { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Material> Materials { get; set; }
        //Tabelanın görseli
        public IFormFile Picture { get; set; }
        //Varsa notlar
        public string Notes { get; set; }
        //Müşterinin Idsi
        public int CustomerId { get; set; }
        //Markanın Idsi
        public int BrandId { get; set; }
        //Malzemenin Idsi
        public int MaterialId { get; set; }
        //Oluşturan kullanıcı
        public int UserId { get; set; }
        public string PicturePath { get; set; }

    }
}
