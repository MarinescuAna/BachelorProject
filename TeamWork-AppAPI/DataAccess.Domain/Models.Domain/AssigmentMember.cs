using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public enum StatusAssigment
    {
        ACTIVE,
        MISSED,
        SENT
    };
    public class AssigmentMember
    {
        [Key]
        public int AssigmentMemberID { get; set; }
        public int AssigmentID { get; set; }
        public Guid AssigmentListUniqueID { get; set; }
        public float TeacherGrade { get; set; }
        public StatusAssigment Status { get; set; }
        public string SolutionLink { get; set; }    
        
        public virtual Assigment Assigment { get; set; }
        public virtual AssigmentList AssigmentList { get; set; }

    }
}
