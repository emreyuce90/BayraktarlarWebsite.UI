using System;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class TabelaDetailViewModel
    {
        public string CustomerName { get; set; }
        public string MaterialName { get; set; }
        public string BrandName { get; set; }
        public string Notes { get; set; }
        public List<string> TabelaImages { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
