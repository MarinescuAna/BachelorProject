using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork_API.ErrorHandler
{
    public static class Conflict409Error
    {

        public static readonly string UserAlreadyExistLogin = "A user has already been created using this email address!";
        public static readonly string PartFromGroup = "You are already part form this group!";
        public static readonly string GroupAlreadyExist = "A group has already been created using this name!";
    }
}
