using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class TabelaUpdateDto
    {
        public int Id { get; set; }
        //liste olarak markalar
        public BrandListDto Brands { get; set; }
        //Markanın Idsi
        public int BrandId { get; set; }
        //Malzemenin Idsi
        public int MaterialId { get; set; }
        public MaterialListDto Materials { get; set; }
        //Tabelanın görseli
        public string Notes { get; set; }

       
        public List<TabelaImages> Images { get; set; }
        public List<IFormFile> AddedPictures { get; set; }

    }
}
