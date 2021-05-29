
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Check
    {
        [Key]
        public Guid CheckID { get; set; }
        public string UserId { get; set; }
        public Guid AssignedTaskID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Description { get; set; }
        public int IsChecked { get; set; }

        public virtual AssignedTask AssignedTask{get;set;}
        public virtual User User { get; set; }
    }
}
