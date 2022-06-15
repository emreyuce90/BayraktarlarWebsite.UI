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
    public class TabelaImagesManager : ITabelaImagesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TabelaImagesManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TabelaImageAddDto tabelaImageAddDto)
        {
            await _unitOfWork.TabelaImages.AddAsync(_mapper.Map<TabelaImages>(tabelaImageAddDto));
        }

        public async Task RemoveAsync(int imageId)
        {
            await _unitOfWork.TabelaImages.DeleteAsync(new TabelaImages { Id = imageId });
            await _unitOfWork.SaveAsync();
        }
    }
}
