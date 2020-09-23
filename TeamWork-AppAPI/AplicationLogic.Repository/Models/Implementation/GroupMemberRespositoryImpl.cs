using AplicationLogic.Repository.Models.Interface;
using DataAccess.Domain.Models.Domain;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicationLogic.Repository.Models.Implementation
{
    public class GroupMemberRespositoryImpl:BaseRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRespositoryImpl(TeamWorkDbContext ctx):base(ctx)
        {

        }
    }
}
