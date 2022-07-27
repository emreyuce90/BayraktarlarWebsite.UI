using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface ILetService
    {
        Task<int> WorkingYearAsync(int userId);
        Task<LetDto> GetAsync(int letId);
        Task<int> RemainingLetAsync(int userId);
        Task ApproveLetAsync(int letId);
        Task<LetListDto> GetAllByUserIdAsync(int userId);
        Task<LetListDto> GetAllAsync();
        Task AddLetAsync(LetAddDto letAddDto);
        Task<int> UsedLetsAsync(int year,int userId);
    }
}
