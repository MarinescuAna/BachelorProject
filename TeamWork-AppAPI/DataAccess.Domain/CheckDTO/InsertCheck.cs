using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.CheckDTO
{
    public class InsertCheck
    {
        public string AssignedTaskId { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
    }
}
