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
    public class LetManager : ILetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LetManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddLetAsync(LetAddDto letAddDto)
        {
            await _unitOfWork.Lets.AddAsync(_mapper.Map<Let>(letAddDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task<LetListDto> GetAllAsync()
        {
            var list = await _unitOfWork.Lets.GetAllAsync();
            return new LetListDto
            {
                Lets = list
            };
        }
    }
}
