using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AssignmentDTO
{
    public class AssignmentDisplay
    {
        public string AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string ChecklistDeadline { get; set; }
        public string GroupsMax { get; set; }
        public string GroupsTake { get; set; }
        public string ListID { get; set; }
        public string CreatedDate { get; set; }
    }
}
