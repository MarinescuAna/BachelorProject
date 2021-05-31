using TeamWork.DataAccess.Repository;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.ApplicationLogger;
using TeamWork.DataAccess.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class GroupRepositoryImpl: BaseRepository<Group>, IGroupRepository
    {
        public GroupRepositoryImpl(TeamWorkDbContext ctx,ILoggerService loggerService): base(ctx,loggerService)
        {

        }

        public override async Task<IEnumerable<Group>> GetItems() =>
            await context.Groups
                .Include(s => s.GroupMembers)
                .ToListAsync();
        public override async Task<Group> GetItem(Expression<Func<Group, bool>> expression) =>
           await context.Groups
           .Include(s => s.GroupMembers)
           .AsNoTracking()
           .FirstOrDefaultAsync(expression);
    }
}
