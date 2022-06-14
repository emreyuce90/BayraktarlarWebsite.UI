using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class TabelaCreateViewModel
    {
        public TabelaImagesListDto TabelaImages { get; set; }
        //Müşteriler
        public CustomerListDto CustomerViewModel { get; set; }
        //Markalar
        public BrandListDto Brands { get; set; }
        //Malzemeler
        public MaterialListDto Materials { get; set; }
        //Seçilen Görseller
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
        //Kayıt edilecek alan
   

    }
}
