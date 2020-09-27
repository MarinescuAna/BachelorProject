using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class AssigmentList
    {
        [Key]
        public Guid AssigmentListUniqueID { get; set; }
        public string DomainName { get; set; }
        public Guid GroupUniqueID { get; set; }
        public int UserID { get; set; }

        public virtual Group Group { get; set; }
        public virtual User Teacher { get; set; }
    }
}
