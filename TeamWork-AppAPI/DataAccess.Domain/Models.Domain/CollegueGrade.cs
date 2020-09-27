
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class CollegueGrade
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }   
        public float Grade { get; set; }
        public int AssigmentID { get; set; }

        public virtual User User { get; set; }
        public virtual Assigment Assigment { get; set; }
    }
}
