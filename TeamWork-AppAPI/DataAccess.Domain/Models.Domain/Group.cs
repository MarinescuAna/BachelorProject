using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class Group
    {
        [Key]
        public Guid GroupUniqueID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public Guid AssigmentListUniqueID { get; set; }
        public AssigmentList AssigmentList { get; set; }
    }
}
