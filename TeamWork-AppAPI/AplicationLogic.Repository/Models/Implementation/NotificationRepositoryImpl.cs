using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class NotificationRepositoryImpl:BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepositoryImpl(TeamWorkDbContext dbContext,ILoggerService logger):
            base(dbContext,logger)
        {

        }
    }
}
