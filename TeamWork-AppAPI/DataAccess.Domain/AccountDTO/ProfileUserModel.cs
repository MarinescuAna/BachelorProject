using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AccountDTO
{
    public class ProfileUserModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Role { get; set; }
        public string ImageContent { get; set; }
        public string ImageExtention { get; set; }
    }
}
