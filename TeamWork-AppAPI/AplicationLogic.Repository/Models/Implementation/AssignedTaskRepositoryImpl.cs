﻿using Microsoft.EntityFrameworkCore;
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
    public class AssignedTaskRepositoryImpl : BaseRepository<AssignedTask>, IAssignedTaskRepository
    {
        public AssignedTaskRepositoryImpl(TeamWorkDbContext workContext, ILoggerService loggerService)
            : base(workContext, loggerService)
        {

        }

        public override async Task<IEnumerable<AssignedTask>> GetItems() =>
            await context.AssignedTasks
               .Include(s => s.CheckListGrades)
               .Include(s => s.PeerEvaluations)
               .Include(s => s.Assignment)
               .Include(s => s.List)
               .Include(s => s.List.Group)
               .ToListAsync();
        public override async Task<AssignedTask> GetItem(Expression<Func<AssignedTask, bool>> expression) =>
            await context.AssignedTasks
            .Include(s => s.List)
            .Include(s => s.List.Group)
            .Include(s => s.Assignment)
            .AsNoTracking()
            .FirstOrDefaultAsync(expression);
    }
}
