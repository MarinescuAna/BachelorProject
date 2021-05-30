using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class CheckListGrade
    {
        [Key]
        public Guid ID { get; set; }
        public string UserID { get; set; }
        public float Grade { get; set; }
        public Guid AssignedTaskID { get; set; }

        public virtual User User { get; set; }
        public virtual AssignedTask AssignedTask { get; set; }
    }
}
