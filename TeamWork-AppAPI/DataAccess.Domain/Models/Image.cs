using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageContent { get; set; }
        public string ImageExtention { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
