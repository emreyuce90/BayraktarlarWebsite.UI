using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface ITabelaService
    {
        Task SoftDeleteAsync(int tabelaId);
        Task<bool> GetStatusCodeGivenTabelaAsync(int tabelaId);
        Task<TabelaListDto> DeletedTabelasAsync(int userId);
        Task UpdateAsync(TabelaUpdateDto tabelaUpdateDto);
        Task<TabelaDto> GetTabelaByTabelaIdAsync(int tabelaId);
        Task<TabelaListDto> GetAllAsync(int userId);
        Task AddAsync(TabelaAddDto tabelaAddDto);
        Task HardDeleteAsync(int tabelaId);
        Task UndoDeleteAsync(int tabelaId);
        Task ChangeStatusAsync(ChangeStatusDto model);
    }
}
