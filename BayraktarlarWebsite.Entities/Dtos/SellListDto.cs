﻿using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class SellListDto
    {
        public IList<Sellout> Sells { get; set; }
        public int Sumtotal { get; set; }
    }
}
