using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class CheckRepositoryImpl:BaseRepository<Check>, ICheckRepository
    {

        public CheckRepositoryImpl(TeamWorkDbContext ctx, ILoggerService loggerService) : base(ctx, loggerService)
        {

        }

        public override async Task<IEnumerable<Check>> GetItems() => await context.Checks
                .Include(s => s.User)
                .Include(s => s.AssignedTask)
                .ToListAsync();
        public override async Task<Check> GetItem(Expression<Func<Check, bool>> expression) =>
            await context.Checks
            .Include(s => s.User)
            .Include(s=>s.AssignedTask)
            .AsNoTracking().FirstOrDefaultAsync(expression);
    }
}
