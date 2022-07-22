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
    public class LetManager : ILetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LetManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddLetAsync(LetAddDto letAddDto)
        {
            await _unitOfWork.Lets.AddAsync(_mapper.Map<Let>(letAddDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task ApproveLetAsync(int letId)
        {
            //Finding let from letId
            var let = await _unitOfWork.Lets.GetAsync(l => l.Id == letId);
            //if let is not null
            if(let != null)
            {
                let.IsApproved = true;
                let.ApprovedDate = DateTime.Now;
                await _unitOfWork.Lets.UpdateAsync(let);
                await _unitOfWork.SaveAsync();
            }

        }

        public async Task<LetListDto> GetAllAsync()
        {
            var list = await _unitOfWork.Lets.GetAllAsync(null, l => l.User);
            return new LetListDto
            {
                Lets = list
            };
        }

        public async Task<LetListDto> GetAllByUserIdAsync(int userId)
        {
            var lets = await _unitOfWork.Lets.GetAllAsync(l => l.UserId == userId);
            return new LetListDto
            {
                Lets = lets
            };
        }

        public async Task<int> UsedLetsAsync(int year, int userId)
        {
           return await _unitOfWork.Lets.SummAsync(l=>l.EndDate.Year == year && l.UserId == userId && l.IsApproved == true,l=>l.DayCount);
            
        }
    }
}
