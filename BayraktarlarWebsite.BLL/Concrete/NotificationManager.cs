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
    public class NotificationManager : INotificationService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           
        }

        public async Task AddNotificationAsync(NotificationAddDto notificationAddDto)
        {
            await _unitOfWork.Notifications.AddAsync(_mapper.Map<Notification>(notificationAddDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task<NotificationListDto> GetAllAsync(int userId)
        {
            var notifications = await _unitOfWork.Notifications.GetAllAsync(n=>n.UserId == userId);
            var sortedList =notifications.OrderBy(n => n.IsRead).ThenByDescending(n=>n.CreatedDate).ToList();
            return new NotificationListDto
            {
                Notifications = sortedList
            };
            
        }

        public async Task ReadAsync(int notificationId)
        {
            var notification = await _unitOfWork.Notifications.GetAsync(n=>n.Id == notificationId);
            notification.IsRead = true;
            await _unitOfWork.Notifications.UpdateAsync(notification);
            await _unitOfWork.SaveAsync();
        }

        public async Task<int> UnreadNotificationsAsync(int userId)
        {
           int result =await _unitOfWork.Notifications.CountAsync(n =>n.IsRead == false && n.UserId == userId);
            return result;
        }
    }
}
