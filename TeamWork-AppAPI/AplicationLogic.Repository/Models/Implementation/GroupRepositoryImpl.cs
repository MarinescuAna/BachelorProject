using AplicationLogic.Repository.Models.Interface;
using DataAccess.Domain.Models.Domain;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicationLogic.Repository.Models.Implementation
{
    public class GroupRepositoryImpl: BaseRepository<Group>, IGroupRepository
    {
        public GroupRepositoryImpl(TeamWorkDbContext ctx): base(ctx)
        {

        }
    }
}
