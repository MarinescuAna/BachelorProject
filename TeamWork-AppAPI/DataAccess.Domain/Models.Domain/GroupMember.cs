
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class GroupMember
    {
        [Key]
        public int GroupMemberID { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
