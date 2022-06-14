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
    public class BrandManager : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BrandManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BrandListDto> GetAllAsync()
        {
            var brands =await _unitOfWork.Brands.GetAllAsync();
            return _mapper.Map<BrandListDto>(brands);
        }
    }
}
