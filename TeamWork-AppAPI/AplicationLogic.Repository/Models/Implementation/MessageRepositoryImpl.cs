using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class MessageRepositoryImpl:BaseRepository<Message>, IMessageRepository
    {
        public MessageRepositoryImpl(TeamWorkDbContext workContext, ILoggerService loggerService):base(workContext, loggerService)
        {

        }

        public override async Task<IEnumerable<Message>> GetItems() => await context.Messages
               .Include(s => s.User)
               .Include(s=>s.Chat)
               .ToListAsync();
    }
}
