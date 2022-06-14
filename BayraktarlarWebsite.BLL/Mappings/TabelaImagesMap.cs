using AutoMapper;
using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Mappings
{
    public class TabelaImagesMap: Profile
    {
        public TabelaImagesMap()
        {
            CreateMap<TabelaImagesDto, TabelaImages>();
            CreateMap<TabelaImages, TabelaImagesDto>();
        }
    }
}
