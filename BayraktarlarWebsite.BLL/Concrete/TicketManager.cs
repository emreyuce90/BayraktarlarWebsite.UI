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
    public class TicketManager : ITicketService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TicketManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddTicketAsync(TicketAddDto ticketAddDto)
        {
            await _unitOfWork.Tickets.AddAsync(_mapper.Map<Ticket>(ticketAddDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task ApproveAsync(int ticketId)
        {
            //retrieving data from db
            var toBeApprovedTicket = await _unitOfWork.Tickets.GetAsync(t => t.Id == ticketId);
            if (toBeApprovedTicket != null)
            {
                toBeApprovedTicket.IsClosed = true;
                await _unitOfWork.Tickets.UpdateAsync(toBeApprovedTicket);
                await _unitOfWork.SaveAsync();
            }

        }

        public async Task<TicketListDto> ClosedTicketsAsync(int userId)
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(t => t.IsClosed == true && t.UserId == userId, t => t.Urgency);
            return new TicketListDto
            {
                Tickets = tickets
            };
        }

        public async Task<int> CountClosedTicketsAsync(int userId)
        {
            //Hatırlatıcıdaki görevleri sayar
            return await _unitOfWork.Tickets.CountAsync(t => t.IsClosed == true && t.UserId == userId);
        }

        public async Task<int> CountNotClosedTicketsAsync(int userId)
        {
            //Hatırlatıcıdaki görevleri sayar
            return await _unitOfWork.Tickets.CountAsync(t => t.IsClosed == false && t.UserId == userId && t.RemainderDate.Date <= DateTime.Now.Date);
        }

        public async Task<int> CountRemainderTicketsAsync(int userId)
        {
            //Hatırlatıcıdaki görevleri sayar
            return await _unitOfWork.Tickets.CountAsync(t => t.IsClosed == false && t.UserId == userId && t.RemainderDate.Date > DateTime.Now.Date);
        }

        public async Task<TicketListDto> GetAllAsync()
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(t => t.IsClosed == false, t => t.Urgency);
            return new TicketListDto
            {
                Tickets = tickets
            };
        }

        public async Task<TicketListDto> GetAllAsync(int userId)
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(t => t.UserId == userId && t.IsClosed == false && t.RemainderDate.Date <= DateTime.Now.Date, t => t.Urgency);

            return new TicketListDto
            {
                Tickets = tickets
            };
        }

        public async Task<TicketListDto> GetAllFutureAsync(int userId)
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(t => t.UserId == userId && t.IsClosed == false && t.RemainderDate.Date > DateTime.Now.Date, t => t.Urgency);

            return new TicketListDto
            {
                Tickets = tickets
            };
        }

        public async Task<TicketUpdateDto> GetAsync(int ticketId)
        {
            var ticket = await _unitOfWork.Tickets.GetAsync(t => t.Id == ticketId);
            return _mapper.Map<TicketUpdateDto>(ticket);
        }

        public async Task UpdateTicketAsync(TicketUpdateDto ticketUpdateDto)
        {
            var oldData = await _unitOfWork.Tickets.GetAsync(t => t.Id == ticketUpdateDto.Id);
            oldData.Subject = ticketUpdateDto.Subject;
            oldData.Detail = ticketUpdateDto.Detail;
            oldData.RemainderDate = ticketUpdateDto.RemainderDate;
            oldData.UrgencyId = ticketUpdateDto.UrgencyId;
            await _unitOfWork.Tickets.UpdateAsync(oldData);
            await _unitOfWork.SaveAsync();
        }
    }
}
