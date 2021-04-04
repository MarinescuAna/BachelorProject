using TeamWork.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.ApplicationLogger;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class GroupMemberRespositoryImpl : BaseRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRespositoryImpl(TeamWorkDbContext ctx, ILoggerService loggerService) : base(ctx,loggerService)
        {

        }

        public override async Task<IEnumerable<GroupMember>> GetItems() => await context.GroupMembers
                .Include(s => s.User)
                .Include(s => s.Group)
                .ToListAsync();
    }
}
