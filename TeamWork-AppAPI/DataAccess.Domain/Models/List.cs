using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamWork.Common.Enums;

namespace TeamWork.DataAccess.Domain.Models
{
    public class List
    {
        [Key]
        public Guid ListID { get; set; }
        public string Domain { get; set; }
        public string Title { get; set; }
        public string UserID { get; set; }
        public DateTime? ListDeadline { get; set; }
        public Guid? GroupID { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Group Group { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<AssignedTask> AssignedTasks { get; set; }
    }
}
