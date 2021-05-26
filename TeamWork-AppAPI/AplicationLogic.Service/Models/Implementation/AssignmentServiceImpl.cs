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
        public async Task<bool> InsertTaskAsync(Assignment list)
        {
            _unitOfWork.Assignment.InsertItem(list);

            return await _unitOfWork.Commit() > 0;
        }
        public async Task<int> TasksAssingedToListAsync(Guid listId) =>
            (await _unitOfWork.Assignment.GetItems()).Where(u => u.ListID == listId).Count();
        public async Task<List<Assignment>> GetAssignmentsByListIdAsync(Guid listId) =>
            (await _unitOfWork.Assignment.GetItems()).Where(u => u.ListID == listId).ToList();
    }
}
