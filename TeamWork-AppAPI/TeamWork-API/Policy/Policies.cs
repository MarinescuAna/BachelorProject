using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork_API.Policy
{
    public class Policies
    {
        public const string Teacher = "Teacher";
        public const string Student = "Student";

        public static AuthorizationPolicy TeacherPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Teacher).Build();
        }

        public static AuthorizationPolicy StudentPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Student).Build();
        }
    }
}
