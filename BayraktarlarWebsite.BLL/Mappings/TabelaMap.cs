﻿using AutoMapper;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Mappings
{
    public class TabelaMap:Profile
    {
        public TabelaMap()
        {
            CreateMap<Tabela,TabelaAddDto>().ReverseMap();
            CreateMap<TabelaDto,Tabela>().ReverseMap();
            CreateMap<Tabela,TabelaUpdateDto>().ReverseMap();
        }
    }
}