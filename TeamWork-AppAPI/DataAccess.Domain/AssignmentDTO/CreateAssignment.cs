using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AssignmentDTO
{
    public class CreateAssignment
    {
        public string ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string ChecklistDeadline { get; set; }
        public string GroupsMax { get; set; }

    }
}
