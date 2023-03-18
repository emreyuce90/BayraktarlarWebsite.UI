﻿using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface ITownService
    {
        Task<List<Town>> GetAllAsync();
    }
}