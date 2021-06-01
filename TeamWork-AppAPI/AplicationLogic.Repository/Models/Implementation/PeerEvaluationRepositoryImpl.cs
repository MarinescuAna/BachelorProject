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
    public class PeerEvaluationRepositoryImpl:BaseRepository<PeerEvaluation>, IPeerEvaluationRepository
    {
        public PeerEvaluationRepositoryImpl(TeamWorkDbContext workContext, ILoggerService loggerService)
            : base(workContext, loggerService)
        {

        }

        public override async Task<IEnumerable<PeerEvaluation>> GetItems() =>
            await context.PeerEvaluations
               .Include(s => s.EvaluatedUser)
               .Include(s => s.AssignedTask)
               .ToListAsync();
        public override async Task<PeerEvaluation> GetItem(Expression<Func<PeerEvaluation, bool>> expression) =>
            await context.PeerEvaluations
               .Include(s => s.EvaluatedUser)
               .Include(s => s.AssignedTask)
               .AsNoTracking()
               .FirstOrDefaultAsync(expression);

    }
}
