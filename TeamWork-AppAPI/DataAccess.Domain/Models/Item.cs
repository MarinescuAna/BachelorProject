using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models
{
    public enum StatusItem
    {
        CHECK,
        UNCHECK
    }
    public class Item
    {
        [Key]
        public Guid ItemID { get; set; }
        public string Description { get; set; }
        public Guid CheckListID { get; set; }
        public StatusItem Status { get; set; }

        public virtual CheckList CheckList { get; set; }
    }
}
