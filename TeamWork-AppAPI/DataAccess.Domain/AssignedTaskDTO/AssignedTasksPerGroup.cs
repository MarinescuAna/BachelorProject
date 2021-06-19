using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AssignedTaskDTO
{
    public class AssignedTasksPerGroup
    {
        public string AssignedTaskId { get; set; }
        public string GroupName { get; set; }
        public string GroupId { get; set; }
        public string AssignmentTitle { get; set; }
        public string AssignmentDeadline { get; set; }
        public string ChkListDeadline { get; set; }
        public string Status { get; set; }
        public string Grade { get; set; }
        public string Solution { get; set; }
        public string StatusChecklist { get; set; }
    }
}
