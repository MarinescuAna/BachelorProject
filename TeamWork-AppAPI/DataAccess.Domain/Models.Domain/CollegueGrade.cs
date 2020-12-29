
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public class CollegueGrade
    {
        [Key]
        public Guid ID { get; set; }
        public string UserId { get; set; }   
        public float Grade { get; set; }
        public Guid AssigmentID { get; set; }

        public virtual User User { get; set; }
        public virtual Assigment Assigment { get; set; }
    }
}
