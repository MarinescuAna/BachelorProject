using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public enum StatusItem
    {
        CHECK,
        UNCHECK
    }
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string Description { get; set; }
        public int CheckListID { get; set; }
        public StatusItem Status { get; set; }

        public virtual CheckList CheckList { get; set; }
    }
}
