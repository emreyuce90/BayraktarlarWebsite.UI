using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class TownManager : ITownService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TownManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Town>> GetAllAsync()
        {
            return await _unitOfWork.Towns.GetAllAsync();
        }
    }
}
