﻿using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.Shared.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfSellRepository:EfEntityRepositoryBase<Sellout>,ISellRepository
    {
        public EfSellRepository(DbContext context):base(context)
        {

        }
    }
}
