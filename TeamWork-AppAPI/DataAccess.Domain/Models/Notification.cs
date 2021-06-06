using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Notification
    {
        public Guid ID { get; set; }
        public string Message { get; set; }
        public string UserID { get; set; }
    }
}
