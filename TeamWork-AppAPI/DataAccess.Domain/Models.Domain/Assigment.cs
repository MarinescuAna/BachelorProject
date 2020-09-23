
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
    public class Assigment
    {
        [Key]
        public int AssigmentID { get; set; }
        public Guid AssigmentListUniqueID { get; set; }
        public AssigmentList AssigmentList { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public int MaxGroup { get; set; }
        public int UserId { get; set; }
        public User Teacher { get; set; }
        public StatusAssigment Status { get; set; }
        public string SolutionLink { get; set; }

    }
}
