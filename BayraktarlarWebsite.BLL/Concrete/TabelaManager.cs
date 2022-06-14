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
    public class TabelaManager : ITabelaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TabelaManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TabelaAddDto tabelaAddDto)
        {
            await _unitOfWork.Tabelas.AddAsync(_mapper.Map<Tabela>(tabelaAddDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task<TabelaListDto> GetAllAsync()
        {
            var tabelas = await _unitOfWork.Tabelas.GetAllAsync(t=>t.IsDeleted==false,t=>t.Brand,t=>t.Customer,t=>t.Material,t=>t.Status,t=>t.Images);
            return _mapper.Map<TabelaListDto>(tabelas);
        }

        public async Task<TabelaDto> GetTabelaByTabelaIdAsync(int tabelaId)
        {
            var tabela = await _unitOfWork.Tabelas.GetAsync(t => t.IsDeleted == false && t.Id ==tabelaId, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images);
            return _mapper.Map<TabelaDto>(tabela);
        }
    }
}
