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
    public class CheckServiceImpl:ABaseService, ICheckService
    {
        public CheckServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public async Task<Check> GetCheckByCheckIdAsync(Guid checkId) =>
            await _unitOfWork.Checks.GetItem(u => u.CheckID == checkId);

        public async Task<List<Check>> GetChecksByEmailAssignedTaskIDAsync(Guid assignedTaskId, string email) =>
            (await _unitOfWork.Checks.GetItems()).Where(u => u.AssignedTaskID == assignedTaskId &&
            u.UserId == email).ToList();

        public async Task<bool> UpdateCheckAsync(Check check)
        {
            await _unitOfWork.Checks.UpdateItem(check);

            return (await _unitOfWork.Commit()) > 0;
        }

        public async Task<bool> InsertCheckAsync(Check check)
        {
             _unitOfWork.Checks.InsertItem(check);

            return (await _unitOfWork.Commit()) > 0;
        }

        public async Task<bool> DeleteCheckAsync(Guid checkId)
        {
            await _unitOfWork.Checks.DeleteItem(u=>u.CheckID==checkId);

            return (await _unitOfWork.Commit()) > 0;
        }
    }
}
