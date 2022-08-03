using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface ITicketService
    {
        Task AddTicketAsync(TicketAddDto ticketAddDto);
        Task<TicketUpdateDto> GetAsync(int ticketId);
        Task ApproveAsync(int ticketId);
        Task<TicketListDto> ClosedTicketsAsync(int userId);
        Task<int> CountRemainderTicketsAsync(int userId);
        Task<int> CountClosedTicketsAsync(int userId);
        Task<int> CountNotClosedTicketsAsync(int userId);
        Task<TicketListDto> GetAllFutureAsync(int userId);
        Task<TicketListDto> GetAllAsync();
        Task<TicketListDto> GetAllAsync(int userId);
        Task UpdateTicketAsync(TicketUpdateDto ticketUpdateDto);
    }
}
