using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class TahsilatManager : ITahsilatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TahsilatManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TahsilatListDto> GetAllAsync(int year)
        {
            var totalList= await _unitOfWork.Tahsilatlar.GetAllAsync(t => t.Date.Year == year,t=>t.User);
            var sumTotalList = await _unitOfWork.Tahsilatlar.SummAsync(t => t.Date.Year == year, t => (decimal)t.Amount);
            return new TahsilatListDto { Tahsilats = totalList ,SumTotal=sumTotalList};
        }

        public async Task<TahsilatListDto> GetAllAsyncByUserId(int userId, int year)
        {
           var list = await _unitOfWork.Tahsilatlar.GetAllAsync(t => t.UserId == userId && t.Date.Year == year);
            var totalSum = await _unitOfWork.Tahsilatlar.SummAsync(t => t.UserId == userId && t.Date.Year == year,t=> (decimal)t.Amount);
            return new TahsilatListDto { Tahsilats = list ,SumTotal =totalSum};
        }
    }
}
