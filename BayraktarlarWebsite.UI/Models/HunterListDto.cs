using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class HunterListDto
    {
        public string TabelaAdi { get; set; }
        public string UstaAdi { get; set; }
        public string SanayiSitesiAdi { get; set; }
        public string Not { get; set; }
        public string CepTelefonu { get; set; }
        public string İl { get; set; }
        public string İlçe { get; set; }
        public List<HunterProduct> HunterProducts { get; set; }
    }
}
