using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageContent { get; set; }
        public string ImageExtention { get; set; }

    }
}
