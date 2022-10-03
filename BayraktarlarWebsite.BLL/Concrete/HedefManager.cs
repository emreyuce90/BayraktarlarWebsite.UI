using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class HedefManager : IHedefService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HedefManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HedefListDto> GetAllByUserIdAsync(int userId)
        {
            var hedefler = await _unitOfWork.Hedefler.GetAllAsync(h => h.UserId == userId);
            return new HedefListDto { HedefList = hedefler };
        }
    }
}
