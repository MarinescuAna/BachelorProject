
using System;
using System.ComponentModel.DataAnnotations;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Message
    {
        [Key]
        public Guid ID { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime? DateSent { get; set; }
        public Guid GroupID { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
