using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Chat
    {
        [Key]
        public Guid ChatID { get; set; }
        public Guid GroupUniqueID { get; set; }

        public virtual Group Group { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
