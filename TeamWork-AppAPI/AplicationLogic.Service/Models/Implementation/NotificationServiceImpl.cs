using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class NotificationServiceImpl:ABaseService,INotificationService
    {
        public NotificationServiceImpl(IUnitOfWork work):
            base(work)
        {
                
        }

        public async Task<bool> DeleteNotificationAsync(Guid notificationId)
        {
            await _unitOfWork.Notifications.DeleteItem(u => u.ID == notificationId);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }

        public async Task<bool> InsertNotificationAsync(Notification notification)
        {
            _unitOfWork.Notifications.InsertItem(notification);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }

        public async Task<List<Notification>> GetNotificationsByEmailAsync(string email) =>
            (await _unitOfWork.Notifications.GetItems()).Where(u => u.UserID == email).ToList();

    }
}
