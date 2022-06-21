using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<int> CountAsync(int statusId,int userId);
        Task<CustomerListDto> GetAllAsync();
    }
}
