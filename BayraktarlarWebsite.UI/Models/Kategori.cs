using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class Kategori
    {
        public string KategoriAdi { get; set; }
        public List<SubProduct> AltKategoriler { get; set; }
    }
}
