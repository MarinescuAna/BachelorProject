
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public int ChatID { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime? DateSent { get; set; }  
        
        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
