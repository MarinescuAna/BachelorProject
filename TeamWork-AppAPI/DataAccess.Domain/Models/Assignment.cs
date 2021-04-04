
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamWork.DataAccess.Domain.Models
{

    public class Assignment
    {
        [Key]
        public Guid AssignmentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public int GroupsMax { get; set; }
        public int GroupsTake { get; set; }
        public string UserID { get; set; }
        public Guid ListID { get; set; }

        public virtual User Teacher { get; set; }
        public virtual List List { get; set; }
        public ICollection<CollegueGrade> CollegueGrades { get; set; }
        public ICollection<AssignedTask> AssignedTasks { get; set; }
    }
}
