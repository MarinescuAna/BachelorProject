using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public class Group
    {
        [Key]
        public Guid GroupUniqueID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
    }
}
