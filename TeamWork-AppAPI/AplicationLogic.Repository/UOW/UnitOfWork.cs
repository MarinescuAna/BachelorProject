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
        private IChatRepository _Chat;
        private IMessageRepository _Message;
        private readonly ILoggerService _loggerService;
        public UnitOfWork(TeamWorkDbContext ctx, ILoggerService loggerService)
        {
            context = ctx;
            _loggerService = loggerService;
        }
        public IChatRepository Chat
        {
            get
            {
                if (_Chat == null)
                {
                    _Chat = new ChatRepositoryImpl(context,_loggerService);
                }

                return _Chat;
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

        public async Task<int> Commit(string loggDetails)
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                LoggMessageError(loggDetails, ex.Message);
                
                if(ex.InnerException!=null)
                {
                    LoggMessageError(loggDetails, ex.InnerException.Message);
                }
                
                Dispose();
            }

            return -1;
        }

        public void LoggMessageError(string path, string message)
        {
            _loggerService.LogError(path,message);
        }
        public async void Dispose()
        {
            await context.DisposeAsync();
        }
    }
}
