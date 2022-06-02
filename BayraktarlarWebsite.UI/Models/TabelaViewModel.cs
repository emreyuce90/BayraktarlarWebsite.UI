using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class TabelaViewModel
    {
        public int Id { get; set; }
        //tabelanın küçük görseli
        public string Thumbnail { get; set; }
        //notu
        public string Notes { get; set; }
        //marka adı
        public string BrandName { get; set; }
        //materyal adı
        public string MaterialName { get; set; }
        //istekte bulunulan tarih
        public DateTime CreatedDate { get; set; }
        //durumu
        public string StatusName { get; set; }
        //talep edilen müşteri
        public string CustomerName { get; set; }


    }
}
