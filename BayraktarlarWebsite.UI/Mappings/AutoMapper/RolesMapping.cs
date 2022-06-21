using AutoMapper;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;

namespace BayraktarlarWebsite.UI.Mappings.AutoMapper
{
    public class RolesMapping:Profile
    {
        public RolesMapping()
        {
            CreateMap<Role, RoleListDto>().ReverseMap();
        }
    }
}
