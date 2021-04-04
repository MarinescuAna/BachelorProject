
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeamWork.Common.Enums;

namespace TeamWork.DataAccess.Domain.Models
{

    public class GroupMember
    {
        [Key]
        public Guid GroupMemberID { get; set; }
        public StatusRequest StatusRequest { get; set; }
        public string UserID { get; set; }
        public Guid GroupID { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }

    }
}
