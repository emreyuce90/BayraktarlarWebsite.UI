using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class HunterManager : IHunterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HunterManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Hunter hunter)
        {
            await _unitOfWork.Hunters.AddAsync(hunter);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Hunter>> GetAll()
        {
            return await _unitOfWork.Hunters.GetAllAsync(null,t=>t.Town,d=>d.District,s=>s.HunterProducts);
        }
    }
}
