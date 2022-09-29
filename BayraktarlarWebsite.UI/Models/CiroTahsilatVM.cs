using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections;

namespace BayraktarlarWebsite.UI.Models
{
    public class CiroTahsilatVM
    {
        public CiroListDto Cirolar { get; set; }
        public TahsilatListDto Tahsilatlar { get; set; }
        public int SelectedYear { get; set; }

    }
}
