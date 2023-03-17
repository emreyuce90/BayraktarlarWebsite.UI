using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface IDistrictService
    {
        Task<List<District>> GetAllAsync();
        Task<List<District>> GetByTownsIdAsync(int townId);
    }
}
