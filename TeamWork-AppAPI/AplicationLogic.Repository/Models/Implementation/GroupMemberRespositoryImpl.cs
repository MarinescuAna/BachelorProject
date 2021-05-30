using TeamWork.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.ApplicationLogger;
using TeamWork.DataAccess.Domain.Models;
using System.Linq.Expressions;
using System;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class GroupMemberRespositoryImpl : BaseRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRespositoryImpl(TeamWorkDbContext ctx, ILoggerService loggerService) 
            : base(ctx,loggerService)
        {

        }

        public override async Task<IEnumerable<GroupMember>> GetItems() =>
            await context.GroupMembers
                .Include(s => s.User)
                .Include(s => s.Group)
                .ToListAsync();
        public override async Task<GroupMember> GetItem(Expression<Func<GroupMember, bool>> expression) =>
           await context.GroupMembers
           .Include(s => s.User)
           .Include(s => s.Group)
           .AsNoTracking()
           .FirstOrDefaultAsync(expression);
    }
}
