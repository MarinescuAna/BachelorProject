using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class AverageServiceImpl:ABaseService,IAverageService
    {
        public AverageServiceImpl(IUnitOfWork work):base(work)
        {

        }

        public async Task<bool> InsertAverageAsync(Average average)
        {
            _unitOfWork.Averages.InsertItem(average);

            return (await _unitOfWork.Commit()) > 0;
        }
        public async Task<Average> GetAverageByAssignedTaskIdAndStudentIdAsync(Guid assignedTaskId, string email)
            => await _unitOfWork.Averages.GetItem(u => u.AssignedTaskID == assignedTaskId && u.StudentID == email);
    }
}
