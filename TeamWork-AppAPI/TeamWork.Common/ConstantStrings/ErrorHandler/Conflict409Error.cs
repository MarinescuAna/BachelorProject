
namespace TeamWork.Common.ConstantStrings.ErrorHandler
{
    public static class Conflict409Error
    {
        public static readonly string PasswordDontMach = "The old password is incorrect!";
        public static readonly string UserAlreadyExistLogin = "A user has already been created using this email address!";
        public static readonly string AssignedTaskAlreadyExistLogin = "This task was already assigned to this list, please choose another one!";
        public static readonly string PartFromGroup = "You are already part form this group!";
        public static readonly string UserIsPartFromGroup = "The user is already part form this group!";
        public static readonly string GroupAlreadyExist = "A group has already been created using this name!";
        public static readonly string AssignmentAlreadyExist = "An assignment has already been created using this name!";
        public static readonly string DeadlineNotSetedExist = "The deadline for this assignment has not been set!";
        public static readonly string EvaluationNotPossible = "The evaluation cannot be done because there are too few members in the group!";
        public static readonly string EvaluationPeerAlreadyDone = "You have already done the evaluation, you cannot evaluate the second time!";
    }
}
