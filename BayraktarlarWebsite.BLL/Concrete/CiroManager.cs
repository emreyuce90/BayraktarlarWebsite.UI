using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Concrete.EntityFrameworkCore;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class CiroManager : ICiroService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CiroManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CiroListDto> GetCiroListAsync(int userId,int year)
        {
            var list =await _unitOfWork.Ciro.GetAllAsync(c=>c.UserId == userId && c.Date.Year == year);
            var total = await _unitOfWork.Ciro.SummAsync(c => c.UserId == userId && c.Date.Year == year,x=> (decimal)x.Amount);
            return new CiroListDto { CiroList = list,SumTotal =total };
        }
    }
}
