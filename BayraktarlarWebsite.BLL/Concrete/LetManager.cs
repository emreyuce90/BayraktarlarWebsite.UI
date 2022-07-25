using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class LetManager : ILetService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LetManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
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
            if (let != null)
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
            var sortedList = list.OrderByDescending(l => l.CreatedDate).ToList();
            return new LetListDto
            {
                Lets = sortedList
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

        public async Task<LetDto> GetAsync(int letId)
        {
            var let = await _unitOfWork.Lets.GetAsync(l=>l.Id == letId);
            return new LetDto
            {
                Let = let
            };
        }

        public async Task<int> RemainingLetAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                int thisYearRight;
                //kullanıcının bu yıl izin hakkı
                if (DateTime.Now.Year - user.EntryDate.Year >= 1 && DateTime.Now.Year - user.EntryDate.Year <= 5)
                {
                    thisYearRight = 14;
                }
                else if (DateTime.Now.Year - user.EntryDate.Year > 5 && DateTime.Now.Year - user.EntryDate.Year <= 15)
                {
                    thisYearRight = 20;
                }
                else if (DateTime.Now.Year - user.EntryDate.Year > 15)
                {
                    thisYearRight = 26;
                }
                else
                {
                    thisYearRight = 0;
                }
                //kullanıcının geçen yıl izin hakkı
                int lastYearRight;

                if ((DateTime.Now.Year - 1) - user.EntryDate.Year >= 1 && ((DateTime.Now.Year - 1) - user.EntryDate.Year <= 5))
                {
                    lastYearRight = 14;
                }
                else if ((DateTime.Now.Year -1) - user.EntryDate.Year > 5 && (DateTime.Now.Year -1) - user.EntryDate.Year <= 15)
                {
                    lastYearRight = 20;
                }
                else if ((DateTime.Now.Year -1) - user.EntryDate.Year > 15)
                {
                    lastYearRight = 26;
                }
                else
                {
                    lastYearRight = 0;
                }
                //kullanıcının bu yıl kullandığı izinler
                var thisYearUsed =await _unitOfWork.Lets.SummAsync(l => l.EndDate.Year == DateTime.Now.Year && l.IsApproved == true && l.UserId == userId,l=>l.DayCount);
                //kullanıcının geçen yıl kullandığı izinler
                var lastYearUsed = await _unitOfWork.Lets.SummAsync(l => l.EndDate.Year == DateTime.Now.Year -1 && l.IsApproved == true && l.UserId == userId, l => l.DayCount);
                int remaining = (lastYearRight - lastYearUsed) + (thisYearRight - thisYearUsed);
                //24
                return remaining;
            }
            return 0;
        }

        public async Task<int> UsedLetsAsync(int year, int userId)
        {
            return await _unitOfWork.Lets.SummAsync(l => l.EndDate.Year == year && l.UserId == userId && l.IsApproved == true, l => l.DayCount);

        }
    }
}
