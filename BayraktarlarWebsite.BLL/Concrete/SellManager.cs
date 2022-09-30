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
    public class SellManager : ISellService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SellListDto> GetAllByUserIdAsync(int userId, int year, int month)
        {
            var salesmanSales =await _unitOfWork.Sells.GetAllAsync(s => s.UserId == userId && s.SellDate.Year == year && s.SellDate.Month == month,s=>s.AltMarka,s=>s.User,s=>s.AltMarka.Marka);
            return new SellListDto { Sells = salesmanSales };
        }
    }
}
