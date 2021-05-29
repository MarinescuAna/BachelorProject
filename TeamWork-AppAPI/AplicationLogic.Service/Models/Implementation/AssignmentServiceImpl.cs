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
    public class AssignmentServiceImpl:ABaseService, IAssignmentService
    {
        public AssignmentServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
        public async Task<Assignment> GetAssignmentByAssignmentIdAsync(Guid assignmentid) =>
         (await _unitOfWork.Assignment.GetItem(u => u.AssignmentID == assignmentid));
        public async Task<bool> UpdateAssignmentAsync(Assignment assignment)
        {
            await _unitOfWork.Assignment.UpdateItem(assignment);

            return (await _unitOfWork.Commit()) > 0;
        }
        public async Task<bool> InsertTaskAsync(Assignment list)
        {
            _unitOfWork.Assignment.InsertItem(list);

            return await _unitOfWork.Commit() > 0;
        }
        public async Task<bool> DeleteAssignmentAsync(Guid assignmentId)
        {
            await _unitOfWork.Assignment.DeleteItem(u => u.AssignmentID == assignmentId);

            return (await _unitOfWork.Commit()) > 0;
        }
        public async Task<Assignment> GetAssignmentByAssignmentTitleAsync(string title) =>
            await _unitOfWork.Assignment.GetItem(u => u.Title == title);
        public async Task<List<Assignment>> GetAssignmentsByListIdAsync(Guid listId) =>
            (await _unitOfWork.Assignment.GetItems()).Where(u => u.ListID == listId).ToList();
    }
}
