using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork_API.ErrorHandler
{
    public static class NotFound404Error
    {
        public static readonly string InvalidEmail = "Invalid email address!";
        public static readonly string InvalidPassword = "Invalid password!";
        public static readonly string InvalidKey = "Invalid key!";
        public static readonly string NotBelongToTeacher = "Email address does not belong to a teacher!";
    }
}
