using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
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
        public Assigment Assigment { get; set; }
        public AssigmentList AssigmentList { get; set; }
        public float TeacherGrade { get; set; }
        public StatusAssigment Status { get; set; }
        public string SolutionLink { get; set; }

    }
}
