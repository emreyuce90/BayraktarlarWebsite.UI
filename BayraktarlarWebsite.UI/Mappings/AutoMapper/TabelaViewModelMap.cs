using AutoMapper;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.UI.Models;

namespace BayraktarlarWebsite.UI.Mappings.AutoMapper
{
    public class TabelaViewModelMap:Profile
    {
        public TabelaViewModelMap()
        {
            CreateMap<TabelaDto, TabelaUpdateDto>().ReverseMap();
            CreateMap<TabelaDto, TabelaUpdateViewModel>().ReverseMap();
            CreateMap<TabelaUpdateDto, TabelaUpdateViewModel>().ReverseMap();
            CreateMap<TabelaListDto, TabelaViewModel>().ReverseMap();
           
        }
    }
}
