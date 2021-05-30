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
    public class CheckListGradeRepositoryImpl:BaseRepository<CheckListGrade>, ICheckListGradeRepository
    {

        public CheckListGradeRepositoryImpl(TeamWorkDbContext ctx, ILoggerService loggerService) : base(ctx, loggerService)
        {

        }

        public override async Task<IEnumerable<CheckListGrade>> GetItems() => await context.CheckListGrades
                .Include(s => s.User)
                .Include(s => s.AssignedTask)
                .ToListAsync();
        public override async Task<CheckListGrade> GetItem(Expression<Func<CheckListGrade, bool>> expression) =>
            await context.CheckListGrades
            .Include(s => s.User)
            .Include(s => s.AssignedTask)
            .AsNoTracking().FirstOrDefaultAsync(expression);
    }
}
