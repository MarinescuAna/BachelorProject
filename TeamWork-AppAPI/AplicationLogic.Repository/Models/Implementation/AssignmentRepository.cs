using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class AssignmentRepository: BaseRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(TeamWorkDbContext teamWorkDbContext, ILoggerService loggerService) 
            : base(teamWorkDbContext, loggerService)
        {

        }

        public override async Task<IEnumerable<Assignment>> GetItems() => 
            await context.Assignments
               .Include(s => s.List)
               .ToListAsync();
        public override async Task<Assignment> GetItem(Expression<Func<Assignment, bool>> expression) => 
            await context.Assignments
            .Include(s => s.List)
            .Include(s => s.AssignedTasks)
            .AsNoTracking()
            .FirstOrDefaultAsync(expression);
    }
}
