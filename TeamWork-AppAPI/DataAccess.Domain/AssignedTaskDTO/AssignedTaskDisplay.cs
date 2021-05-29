using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AssignedTaskDTO
{
    public class AssignedTaskDisplay
    {
        
        public string AssignedTaskId { get; set; }
        public string AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string ChecklistDeadline { get; set; }
        public string ListID { get; set; }
        public string TeacherGrade { get; set; }
        public string SolutionLink { get; set; }
        public string StatusTask { get; set; }
    }
}
