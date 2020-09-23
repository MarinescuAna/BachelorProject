using DataAccess.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Domain.Account.Domain
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Institution { get; set; }
        public string UserRole { get; set; }
    }
}
