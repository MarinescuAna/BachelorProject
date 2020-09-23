using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class AssigmentMember
    {
        [Key]
        public int AssigmentMemberID { get; set; }
        public int AssigmentID { get; set; }
        public Assigment Assigment { get; set; }
        public Guid AssigmentListUniqueID { get; set; }
        public AssigmentList AssigmentList { get; set; }
        public Guid GroupUniqueID { get; set; }
        public Group Group { get; set; }
        public float TeacherGrade { get; set; }
    }
}
