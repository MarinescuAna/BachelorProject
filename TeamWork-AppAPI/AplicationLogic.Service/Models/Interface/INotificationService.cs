using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface INotificationService
    {
        Task<bool> DeleteNotificationAsync(Guid notificationId);
        Task<bool> InsertNotificationAsync(Notification notification);
        Task<List<Notification>> GetNotificationsByEmailAsync(string email);
    }
}
