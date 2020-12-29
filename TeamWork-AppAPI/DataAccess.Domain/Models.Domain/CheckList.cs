
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public enum StatusChecklist
    {
        APPROVE,
        IN_PROGRESS,
        UNNECESSARY
    };
    public class CheckList
    {
        [Key]
        public Guid CheckListID { get; set; }
        public string UserId { get; set; }
        public StatusChecklist Status { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
