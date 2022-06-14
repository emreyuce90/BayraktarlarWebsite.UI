using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class TabelaUpdateViewModel
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
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }

        public TabelaImagesListDto Pictures { get; set; }
        public List<IFormFile> AddedPictures { get; set; }
    }
}
