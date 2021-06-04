using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IAverageService
    {
        Task<bool> InsertAverageAsync(Average average);
        Task<Average> GetAverageByAssignedTaskIdAndStudentIdAsync(Guid assignedTaskId, string email);
    }
}
