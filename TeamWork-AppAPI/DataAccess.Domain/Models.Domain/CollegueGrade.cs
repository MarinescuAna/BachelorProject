
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
        public User User { get; set; }
        public float Grade { get; set; }
        public Assigment Assigment { get; set; }
    }
}
