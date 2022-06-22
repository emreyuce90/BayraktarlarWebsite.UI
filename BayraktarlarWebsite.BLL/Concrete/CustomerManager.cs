using AutoMapper;
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
    public class CustomerManager : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountAsync(int statusId,int userId)
        {
            //sana gelen status id ye ait kayıtları say
            return await _unitOfWork.Tabelas.CountAsync(t => t.StatusId == statusId && t.UserId == userId);

        }

        public async Task<int> CountAsync(int statusId)
        {
            //sana gelen status id ye ait kayıtları say
            return await _unitOfWork.Tabelas.CountAsync(t => t.StatusId == statusId);
        }

        public async Task<CustomerListDto> GetAllAsync()
        {
            //db den gelen müşteriler
            var customers =await _unitOfWork.Customer.GetAllAsync();
            //db den gelen Customer listesini map ile dto ya çevirip return et
            return new CustomerListDto
            {
                Customers = customers
            };
        }
    }
}
