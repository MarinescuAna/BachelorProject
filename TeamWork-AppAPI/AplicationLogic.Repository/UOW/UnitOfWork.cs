using AplicationLogic.Repository.Models.Implementation;
using AplicationLogic.Repository.Models.Interface;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TeamWorkDbContext context;
        private IUserRepository _User;
        private IGroupRepository _Group;
        private IGroupMemberRepository _GroupMember;
        public UnitOfWork(TeamWorkDbContext ctx)
        {
            context = ctx;
        }
        public IUserRepository User {
            get
            {
                if(_User==null)
                {
                    _User = new UserRepositoryImpl(context);
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
                    _GroupMember = new GroupMemberRespositoryImpl(context);
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
                    _Group = new GroupRepositoryImpl(context);
                }

                return _Group;
            }
        }

        public async Task<int> Commit()
        {
           return await context.SaveChangesAsync();
        }

        public async void Dispose()
        {
             await context.DisposeAsync();
        }
    }
}
