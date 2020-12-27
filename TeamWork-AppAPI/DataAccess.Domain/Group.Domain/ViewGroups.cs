using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Group.Domain
{
    public class ViewGroups
    {
        public string UniqueKey { get; set; }
        public string GroupName { get; set; }
        public string TeacherName { get; set; }
        public string NoMembers { get; set; }
        public string TeacherEmail { get; set; }
        public string GroupDetails { get; set; }
    }
}
