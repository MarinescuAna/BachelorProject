using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class AssignedTaskServiceImpl:ABaseService, IAssignedTaskService
    {
        public AssignedTaskServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public async Task<bool> DeleteTaskAssignedByIdAsync(Guid guid)
        {
            await _unitOfWork.AssignedTasks.DeleteItem(u => u.AssignedTaskID == guid);

            return (await _unitOfWork.Commit()) > 0;
        }
        public async Task<bool> AssignTaskAsync(AssignedTask assigned)
        {
            _unitOfWork.AssignedTasks.InsertItem(assigned);

            return (await _unitOfWork.Commit()) > 0;
        }
        public async Task<AssignedTask> GetAssignedByIdAsync(Guid id) => await _unitOfWork.AssignedTasks.GetItem(
            u => u.AssignedTaskID == id);
        public async Task<List<AssignedTask>> GetAssignedTasksByListIdAsync(Guid listId) =>
            (await _unitOfWork.AssignedTasks.GetItems()).Where(u => u.ListID == listId).ToList();

        public async Task<List<AssignedTask>> GetAssignedTasksByAssignmentIdAsync(Guid assignmentId)
            => (await _unitOfWork.AssignedTasks.GetItems()).Where(u=>u.AssignmentID==assignmentId).ToList();
        public async Task<List<AssignedTask>> GetAssignedTasksPerGroupsByListIdAsync(Guid listId) =>
            (await _unitOfWork.AssignedTasks.GetItems()).Where(u => u.Assignment.ListID == listId).ToList();
        public async Task<bool> UpdateTaskAsync(AssignedTask assignedTask)
        {
            await _unitOfWork.AssignedTasks.UpdateItem(assignedTask);

            return (await _unitOfWork.Commit()) > 0;

        }
    }
}
