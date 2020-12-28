using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Group.Domain
{
    public class GroupUpdateReceived
    {
        public string GroupName { get; set; }
        public string OldGroupName { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }
}
