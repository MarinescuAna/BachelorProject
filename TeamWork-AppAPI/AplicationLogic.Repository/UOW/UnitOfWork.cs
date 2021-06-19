using System;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Implementation;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TeamWorkDbContext context;
        private IUserRepository _User;
        private IGroupRepository _Group;
        private IGroupMemberRepository _GroupMember;
        private IMessageRepository _Message;
        private IImageRepository _Image;
        private IAssignmentRepository _Assignment;
        private IListRepository _List;
        private IAssignedTaskRepository _AssignedTask;
        private ICheckRepository _Check;
        private INotificationRepository _Notification;
        private IAverageRepository _Average;
        private IPeerEvaluationRepository _PeerEvaluation;
        private ICheckListGradeRepository _CheckListGrade;
        private readonly ILoggerService _loggerService;
        public UnitOfWork(TeamWorkDbContext ctx, ILoggerService loggerService)
        {
            context = ctx;
            _loggerService = loggerService;
        }
        public INotificationRepository Notifications
        {
            get
            {
                if (_Notification == null)
                {
                    _Notification = new NotificationRepositoryImpl(context, _loggerService);
                }

                return _Notification;
            }
        }
        public IAverageRepository Averages
        {
            get
            {
                if (_Average == null)
                {
                    _Average = new AverageRepositoryImpl(context, _loggerService);
                }

                return _Average;
            }
        }
        public IPeerEvaluationRepository PeerEvaluations
        {
            get
            {
                if (_PeerEvaluation == null)
                {
                    _PeerEvaluation = new PeerEvaluationRepositoryImpl(context, _loggerService);
                }

                return _PeerEvaluation;
            }
        }
        public ICheckListGradeRepository CheckListGrades
        {
            get
            {
                if (_CheckListGrade == null)
                {
                    _CheckListGrade = new CheckListGradeRepositoryImpl(context, _loggerService);
                }

                return _CheckListGrade;
            }
        }
        public ICheckRepository Checks
        {
            get
            {
                if (_Check == null)
                {
                    _Check = new CheckRepositoryImpl(context, _loggerService);
                }

                return _Check;
            }
        }
        public IAssignedTaskRepository AssignedTasks
        {
            get
            {
                if (_AssignedTask == null)
                {
                    _AssignedTask = new AssignedTaskRepositoryImpl(context, _loggerService);
                }

                return _AssignedTask;
            }
        }
        public IAssignmentRepository Assignment
        {
            get
            {
                if (_Assignment == null)
                {
                    _Assignment = new AssignmentRepository(context, _loggerService);
                }

                return _Assignment;
            }
        }
        public IListRepository List
        {
            get
            {
                if (_List == null)
                {
                    _List = new ListRepository(context, _loggerService);
                }

                return _List;
            }
        }
        public IImageRepository Image
        {
            get
            {
                if (_Image == null)
                {
                    _Image = new ImageRepositoryImpl(context, _loggerService);
                }

                return _Image;
            }
        }
        public IMessageRepository Message
        {
            get
            {
                if (_Message == null)
                {
                    _Message = new MessageRepositoryImpl(context, _loggerService);
                }

                return _Message;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_User == null)
                {
                    _User = new UserRepositoryImpl(context, _loggerService);
                }

                return _User;
            }
        }
        public IGroupMemberRepository GroupMember
        {
            get
            {
                if (_GroupMember == null)
                {
                    _GroupMember = new GroupMemberRespositoryImpl(context, _loggerService);
                }

                return _GroupMember;
            }
        }
        public IGroupRepository Group
        {
            get
            {
                if (_Group == null)
                {
                    _Group = new GroupRepositoryImpl(context, _loggerService);
                }

                return _Group;
            }
        }

        public async Task<int> Commit()
        {
            try
            {
                LoggMessageInfo("Try to commit the changes.");
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                LoggMessageError($"The commit faild because the follow error occures: {ex.Message}");
                
                if(ex.InnerException!=null)
                {
                    LoggMessageError($" Inner Exception Message: {ex.InnerException.Message}");
                }
                
                Dispose();
            }

            return -1;
        }

        public void LoggMessageError(string message)
        {
            _loggerService.LogError(message);
        }
        public void LoggMessageInfo(string message)
        {
            _loggerService.LogInfo(message);
        }
        public async void Dispose()
        {
            LoggMessageInfo("Dispose.");
            await context.DisposeAsync();
        }
    }
}
