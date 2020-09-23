using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Domain.Group.Domain
{
    public class GroupDetalisReceived
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string TeacherEmail { get; set; }
        public string StudentCreatorEmail { get; set; }
    }
}
