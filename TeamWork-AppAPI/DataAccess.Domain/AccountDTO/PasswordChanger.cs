using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AccountDTO
{
    public class PasswordChanger
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
