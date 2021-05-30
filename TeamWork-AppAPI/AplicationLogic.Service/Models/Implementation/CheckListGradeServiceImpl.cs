using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class CheckListGradeServiceImpl: ABaseService, ICheckListGradeService
    {
        public CheckListGradeServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public async Task<bool> InsertCheckListGradeAsync(CheckListGrade checkListGrade)
        {
            _unitOfWork.CheckListGrades.InsertItem(checkListGrade);

            return (await _unitOfWork.Commit()) > 0;
        }
    }
}
