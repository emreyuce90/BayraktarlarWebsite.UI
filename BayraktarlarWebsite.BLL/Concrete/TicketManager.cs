using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
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

        public Task AddTicketAsync(TicketAddDto ticketAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<TicketListDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TicketListDto> GetAllAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
