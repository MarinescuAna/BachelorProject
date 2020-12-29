
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{

    public class Assigment
    {
        [Key]
        public Guid AssigmentID { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public int MaxGroup { get; set; }
        public string UserID { get; set; }

        public virtual User Teacher { get; set; }

        public ICollection<CollegueGrade> CollegueGrades { get; set; }
        public ICollection<AssigmentMember> AssigmentMembers { get; set; }
    }
}
