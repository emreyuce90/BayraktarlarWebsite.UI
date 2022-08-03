using AutoMapper;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Mappings
{
    public class TicketMap:Profile
    {
        public TicketMap()
        {
            CreateMap<TicketAddDto, Ticket>().ReverseMap();
            CreateMap<TicketUpdateDto, Ticket>().ReverseMap();
        }
    }
}
