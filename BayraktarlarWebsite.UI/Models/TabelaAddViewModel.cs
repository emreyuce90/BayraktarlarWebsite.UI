using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class TabelaAddViewModel
    {
        //Tabela
        public TabelaCreateViewModel TabelaCreateViewModel { get; set; }
        //TabelaImages
        public List<IFormFile>  TabelaImages { get; set; }
    }
}
