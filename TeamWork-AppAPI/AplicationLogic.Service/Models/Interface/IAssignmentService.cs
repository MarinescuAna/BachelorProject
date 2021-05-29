﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IAssignmentService
    {
        Task<bool> UpdateAssignmentAsync(Assignment assignment);
        Task<Assignment> GetAssignmentByAssignmentIdAsync(Guid assignmentid);
        Task<bool> InsertTaskAsync(Assignment list);
        Task<List<Assignment>> GetAssignmentsByListIdAsync(Guid listId);
        Task<Assignment> GetAssignmentByAssignmentTitleAsync(string title);
        Task<bool> DeleteAssignmentAsync(Guid assignmentId);
    }
}