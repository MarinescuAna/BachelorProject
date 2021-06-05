using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.GroupDTO
{
    public class RandomGroupsCreate
    {
        public List<string> Emails  { get; set; }
        public List<string> GroupNames  { get; set; }
        public string NumberMax  { get; set; }
        public string Error  { get; set; }
    }
}
