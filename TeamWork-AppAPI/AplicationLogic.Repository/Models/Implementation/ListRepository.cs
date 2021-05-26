using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class ListRepository: BaseRepository<List>, IListRepository
    {
        public ListRepository(TeamWorkDbContext teamWorkDbContext, ILoggerService loggerService) : base(teamWorkDbContext, loggerService)
        {

        }

        public override async Task<IEnumerable<List>> GetItems() => await context.List
             .Include(s => s.Teacher)
             .ToListAsync();
    }
}

