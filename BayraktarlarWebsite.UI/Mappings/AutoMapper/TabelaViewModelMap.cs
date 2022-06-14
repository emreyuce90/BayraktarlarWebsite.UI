using AutoMapper;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.UI.Models;

namespace BayraktarlarWebsite.UI.Mappings.AutoMapper
{
    public class TabelaViewModelMap:Profile
    {
        public TabelaViewModelMap()
        {
            CreateMap<TabelaDto, TabelaUpdateViewModel>().ReverseMap();
        }
    }
}
