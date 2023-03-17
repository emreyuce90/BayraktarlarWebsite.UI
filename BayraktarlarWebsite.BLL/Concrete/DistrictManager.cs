using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class DistrictManager : IDistrictService
    {
        private readonly IUnitOfWork _unitfWork;

        public DistrictManager(IUnitOfWork unitfWork)
        {
            _unitfWork = unitfWork;
        }

        public async Task<List<District>> GetAllAsync()
        {
           var districts =  await _unitfWork.Districts.GetAllAsync();
            return districts;
        }

        public async Task<List<District>> GetByTownsIdAsync(int townId)
        {
           return await _unitfWork.Districts.GetAllAsync(d=>d.TownId == townId);
        }
    }
}
