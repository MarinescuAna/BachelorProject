using System;
using System.Collections.Generic;
using System.Text;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class NotificationServiceImpl:ABaseService,INotificationService
    {
        public NotificationServiceImpl(IUnitOfWork work):
            base(work)
        {
                
        }
    }
}
