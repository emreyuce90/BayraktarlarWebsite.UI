using BayraktarlarWebsite.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Interfaces
{
    public interface INotificationService
    {
        Task ReadAsync(int notificationId);
        Task<int> UnreadNotificationsAsync(int userId);
        Task AddNotificationAsync(NotificationAddDto notificationAddDto);
        Task<NotificationListDto> GetAllAsync(int userId);
    }
}
