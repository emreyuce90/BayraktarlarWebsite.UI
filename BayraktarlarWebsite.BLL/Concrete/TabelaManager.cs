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

        public async Task ChangeStatusAsync(ChangeStatusDto model)
        {
            var tabela =await _unitOfWork.Tabelas.GetAsync(t=>t.Id == model.TabelaId);
            if(tabela !=null)
            {
                tabela.StatusId = model.StatusId;
                await _unitOfWork.Tabelas.UpdateAsync(tabela);
                await _unitOfWork.SaveAsync();
            }
            
        }

        public async Task<TabelaListDto> DeletedTabelasAsync(int userId)
        {
            var tabelas = await _unitOfWork.Tabelas.GetAllAsync(t => t.IsDeleted == true && t.UserId == userId, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images,t=>t.User);
            return new TabelaListDto
            {
                Tabela = tabelas
            };
        }

        public async Task<TabelaListDto> DeletedTabelasAsync()
        {
            var tabelas = await _unitOfWork.Tabelas.GetAllAsync(t => t.IsDeleted == true, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images, t => t.User);
            return new TabelaListDto
            {
                Tabela = tabelas
            };
        }

        public async Task<TabelaListDto> GetAllAsync(int userId)
        {
            var tabelas = await _unitOfWork.Tabelas.GetAllAsync(t => t.IsDeleted == false && t.UserId== userId, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images);
            return new TabelaListDto
            {
                Tabela = tabelas
            };
        }

        public async Task<TabelaListDto> GetAllAsync()
        {
            var tabelas = await _unitOfWork.Tabelas.GetAllAsync(t => t.IsDeleted == false, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images,t=>t.User);
            return new TabelaListDto
            {
                Tabela = tabelas
            };
        }

        public async Task<bool> GetStatusCodeGivenTabelaAsync(int tabelaId)
        {
            var tabela = await _unitOfWork.Tabelas.GetAsync(t => t.Id == tabelaId);
            if(tabela.StatusId == 5)
            {
                return true;
            }
            return false;
        }

        public async Task<TabelaDto> GetTabelaByTabelaIdAsync(int tabelaId)
        {
            var tabela = await _unitOfWork.Tabelas.GetAsync(t=>t.Id == tabelaId, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images);
            return new TabelaDto { Tabela = tabela};
        }

        public async Task HardDeleteAsync(int tabelaId)
        {
            var id = await _unitOfWork.Tabelas.AnyAsync(t => t.Id == tabelaId);
            if (id)
            {
                await _unitOfWork.Tabelas.DeleteAsync(new Tabela { Id = tabelaId });
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task SoftDeleteAsync(int tabelaId)
        {
            //gelen idye ait olan tabela
            var id = await _unitOfWork.Tabelas.GetAsync(t => t.Id == tabelaId);
            id.IsDeleted = true;
            await _unitOfWork.Tabelas.UpdateAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UndoDeleteAsync(int tabelaId)
        {
            var undoDeleteTabela = await _unitOfWork.Tabelas.GetAsync(t => t.Id == tabelaId);
            if(undoDeleteTabela != null)
            {
                undoDeleteTabela.IsDeleted = false;
                await _unitOfWork.Tabelas.UpdateAsync(undoDeleteTabela);
                await _unitOfWork.SaveAsync();
            }

            

        }

        public async Task UpdateAsync(TabelaUpdateDto tabelaUpdateDto)
        {
            var oldTabela = await _unitOfWork.Tabelas.GetAsync(t => t.Id == tabelaUpdateDto.Id, t => t.Brand, t => t.Customer, t => t.Material, t => t.Status, t => t.Images);
            var newTabela = _mapper.Map<TabelaUpdateDto, Tabela>(tabelaUpdateDto, oldTabela);
            newTabela.ModifiedDate = DateTime.Now;
            await _unitOfWork.Tabelas.UpdateAsync(newTabela);
            await _unitOfWork.SaveAsync();
        }
    }
}
