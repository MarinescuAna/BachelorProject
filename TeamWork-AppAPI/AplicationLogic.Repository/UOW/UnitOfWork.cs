﻿using AplicationLogic.Repository.Models.Implementation;
using AplicationLogic.Repository.Models.Interface;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.AplicationLogin.Logger;

namespace AplicationLogic.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TeamWorkDbContext context;
        private IUserRepository _User;
        private IGroupRepository _Group;
        private IGroupMemberRepository _GroupMember;
        private readonly ILoggerService _loggerService;
        public UnitOfWork(TeamWorkDbContext ctx, ILoggerService loggerService)
        {
            context = ctx;
            _loggerService = loggerService;
        }
        public IUserRepository User
        {
            get
            {
                if (_User == null)
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

        public async Task<int> Commit(string loggDetails)
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(loggDetails, ex.Message);
                Dispose();
            }

            return -1;
        }

        public async void Dispose()
        {
            await context.DisposeAsync();
        }
    }
}
