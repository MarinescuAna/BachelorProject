﻿using System;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.Models.Interface;

namespace TeamWork.ApplicationLogic.Repository.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository User { get; }
        IGroupRepository Group { get; }
        IGroupMemberRepository GroupMember { get; }
        Task<int> Commit(string loggDetails);
        void LoggMessageError(string path, string message);
    }
}
