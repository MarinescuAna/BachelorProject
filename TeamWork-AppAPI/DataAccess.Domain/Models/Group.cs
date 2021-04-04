using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Group
    {
        [Key]
        public Guid GroupUniqueID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public ICollection<List> Lists { get; set; }
    }
}
