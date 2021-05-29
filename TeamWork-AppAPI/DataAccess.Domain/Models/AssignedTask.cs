using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class AssignedTask
    {
        [Key]
        public Guid AssignedTaskID { get; set; }
        public Guid AssignmentID { get; set; }
        public Guid ListID { get; set; }
        public float TeacherGrade { get; set; }
        public string SolutionLink { get; set; }

        public virtual List List { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}
