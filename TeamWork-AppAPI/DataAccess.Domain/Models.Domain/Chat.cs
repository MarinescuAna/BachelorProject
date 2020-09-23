using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class Chat
    {
        [Key]
        public int ChatID { get; set; }
        public Group Group { get; set; }
    }
}
