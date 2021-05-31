using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface ICheckService
    {
        Task<Check> GetCheckByCheckIdAsync(Guid checkId);
        Task<List<Check>> GetChecksByEmailAssignedTaskIDAsync(Guid assignedTaskId, string email);
        Task<bool> UpdateCheckAsync(Check check);
        Task<bool> InsertCheckAsync(Check check);
        Task<bool> DeleteCheckAsync(Guid checkId);
        Task<(int totalTasks, int tasksDone)> ReportChecksByUserIdAssignedTaskIdAsync(Guid assignedTaskId, string userId);
    }
}
