
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class PeerEvaluation
    {
        [Key]
        public Guid ID { get; set; }
        public string EvaluatingUserEmail { get; set; }   
        public string UserID { get; set; }
        public string Comments { get; set; }
        public float Grade { get; set; }
        public Guid AssignedTaskID { get; set; }

        public virtual User EvaluatedUser { get; set; }
        public virtual AssignedTask AssignedTask { get; set; }
    }
}
