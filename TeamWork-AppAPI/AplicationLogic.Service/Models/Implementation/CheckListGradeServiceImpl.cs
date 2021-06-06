using System;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class CheckListGradeServiceImpl : ABaseService, ICheckListGradeService
    {
        public CheckListGradeServiceImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<bool> InsertCheckListGradeAsync(CheckListGrade checkListGrade)
        {
            _unitOfWork.CheckListGrades.InsertItem(checkListGrade);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }

        public async Task<CheckListGrade> GetCheckListGradeByUserIdAssignedTaskIDAsync(Guid assignedTaskID, string userID)
            => await _unitOfWork.CheckListGrades.GetItem(u => u.AssignedTaskID == assignedTaskID && u.UserID == userID);
    }
}
