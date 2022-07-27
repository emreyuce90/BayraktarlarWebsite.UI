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
    
    public class UrgencyManager : IUrgencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UrgencyManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UrgencyListDto> GetAllAsync()
        {
            var urgencies = await _unitOfWork.Urgencies.GetAllAsync();
            return new UrgencyListDto
            {
                Urgencies = urgencies
            };
        }
    }
}
