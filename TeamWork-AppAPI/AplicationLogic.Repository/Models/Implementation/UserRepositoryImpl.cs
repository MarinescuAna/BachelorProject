using TeamWork.DataAccess.Repository;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork.ApplicationLogger;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class UserRepositoryImpl :BaseRepository<User>, IUserRepository
    {
        public UserRepositoryImpl(TeamWorkDbContext ctx, ILoggerService loggerService):base(ctx, loggerService)
        {
           
        }
        public override async Task<IEnumerable<User>> GetItems() => await context.Users
                .Include(s => s.Image)
                .ToListAsync();

    }
}
