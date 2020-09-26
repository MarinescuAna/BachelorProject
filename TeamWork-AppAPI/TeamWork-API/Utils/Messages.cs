using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork_API.Utils
{
    public static class Messages
    {
        public static readonly string InvalidCredentials_4040NotFound = "Invalid credentials!";
        public static readonly string NotBelongToTeacher_4040NotFound = "Email address does not belong to a teacher!";
        public static readonly string SthWentWrong_400BadRequest = "Something went wrong! Please try again";
        public static readonly string NoContent_204NoContent = "No content! Try again.";
        public static readonly string UserAlreadyExistLogin_409Conflict = "A user has already been created using this email address!";
        public static readonly string GroupAlreadyExist_409Conflict = "A group has already been created using this name!";

    }
}
