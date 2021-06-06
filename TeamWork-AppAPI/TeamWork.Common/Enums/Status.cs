using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.Common.Enums
{
    public enum Role
    {
        STUDENT,
        TEACHER
    };
    public enum StatusRequest
    {
        Joined,
        Waiting,
        Declined
    }

    public enum DeadlineStatus
    {
        ACTIVE,
        PASS,
        DONE
    }

    public enum AssignmentStatus
    {
        TAKEN,
        ACTIVE,
        PASS
    }
}
