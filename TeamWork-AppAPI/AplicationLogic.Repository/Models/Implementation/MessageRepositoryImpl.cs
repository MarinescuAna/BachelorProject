using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class MessageRepositoryImpl:BaseRepository<Message>, IMessageRepository
    {
        public MessageRepositoryImpl(TeamWorkDbContext workContext, ILoggerService loggerService)
            :base(workContext, loggerService)
        {

        }

        public override async Task<IEnumerable<Message>> GetItems() =>
            await context.Messages
               .Include(s => s.User)
               .Include(s=>s.Chat)
               .ToListAsync();
        public override async Task<Message> GetItem(Expression<Func<Message, bool>> expression) =>
           await context.Messages
           .Include(s => s.User)
           .Include(s => s.Chat)
           .AsNoTracking()
           .FirstOrDefaultAsync(expression);

    }
}
