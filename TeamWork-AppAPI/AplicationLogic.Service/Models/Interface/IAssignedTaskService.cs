using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IAssignedTaskService
    {
        Task<List<AssignedTask>> GetAssignedTasksByListIdAsync(Guid listId);
        Task<List<AssignedTask>> GetAssignedTasksByAssignmentIdAsync(Guid assignmentId);
        Task<bool> AssignTaskAsync(AssignedTask assigned);
        Task<bool> UpdateTaskAsync(AssignedTask assignedTask);
        Task<AssignedTask> GetAssignedByIdAsync(Guid id);
        Task<bool> DeleteTaskAssignedByIdAsync(Guid guid);
        Task<List<AssignedTask>> GetAssignedTasksPerGroupsByListIdAsync(Guid listId);
    }
}
