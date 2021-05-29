using System;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.Models.Interface;

namespace TeamWork.ApplicationLogic.Repository.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository User { get; }
        IListRepository List { get; }
        IAssignmentRepository Assignment { get; }
        IImageRepository Image { get; }
        IChatRepository Chat { get; }
        IMessageRepository Message { get; }
        IGroupRepository Group { get; }
        IAssignedTaskRepository AssignedTasks { get; }
        IGroupMemberRepository GroupMember { get; }
        Task<int> Commit();
        void LoggMessageError(string message);
        void LoggMessageInfo(string message);
    }
}
