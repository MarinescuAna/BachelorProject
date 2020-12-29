
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public enum StatusRequest
    {
        Joined,
        Waiting,
        Declined
    }
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
