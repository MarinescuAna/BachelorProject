using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.CheckDTO
{
    public class CheckDisplay
    {
        public string CheckID { get; set; }
        public string CreationDate { get; set; }
        public string LastUpdate { get; set; }
        public string Description { get; set; }
        public string IsChecked { get; set; }
    }
}
