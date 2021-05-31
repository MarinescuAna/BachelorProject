using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface ICheckListGradeService
    {
        Task<bool> InsertCheckListGradeAsync(CheckListGrade checkListGrade);
        Task<CheckListGrade> GetCheckListGradeByUserIdAssignedTaskIDAsync(Guid assignedTaskID, string userID);
    }
}
