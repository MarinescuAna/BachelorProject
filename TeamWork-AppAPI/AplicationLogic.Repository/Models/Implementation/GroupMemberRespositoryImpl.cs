using TeamWork.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class GroupMemberRespositoryImpl : BaseRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRespositoryImpl(TeamWorkDbContext ctx) : base(ctx)
        {

        }

        public override async Task<IEnumerable<GroupMember>> GetItems() => await context.GroupMembers
                .Include(s => s.User)
                .Include(s => s.Group)
                .ToListAsync();
    }
}
