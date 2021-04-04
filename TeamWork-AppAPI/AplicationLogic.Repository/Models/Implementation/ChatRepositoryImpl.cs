using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class ChatRepositoryImpl : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepositoryImpl(TeamWorkDbContext teamWorkDbContext, ILoggerService loggerService ):base(teamWorkDbContext, loggerService)
        {

        }

        public override async Task<IEnumerable<Chat>> GetItems() => await context.Chats
               .Include(s => s.Group)
               .ToListAsync();

    }
}
