using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Average
    {
        public Guid Id { get; set; }
        public string TeacherID { get; set; }
        public string StudentID { get; set; }
        public Guid AssignedTaskID { get; set; }
        public float GradePerAssignedTask { get; set; }
    }
}
