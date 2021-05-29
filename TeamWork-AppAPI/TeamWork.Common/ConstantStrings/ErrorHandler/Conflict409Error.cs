﻿
namespace TeamWork.Common.ConstantStrings.ErrorHandler
{
    public static class Conflict409Error
    {

        public static readonly string UserAlreadyExistLogin = "A user has already been created using this email address!";
        public static readonly string PartFromGroup = "You are already part form this group!";
        public static readonly string UserIsPartFromGroup = "The user is already part form this group!";
        public static readonly string GroupAlreadyExist = "A group has already been created using this name!";
        public static readonly string AssignmentAlreadyExist = "An assignment has already been created using this name!";
        public static readonly string DeadlineNotSetedExist = "The deadline for this assignment has not been set.!";
    }
}