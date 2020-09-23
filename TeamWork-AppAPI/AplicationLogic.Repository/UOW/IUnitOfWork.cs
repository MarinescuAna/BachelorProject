﻿using AplicationLogic.Repository.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Repository.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository User { get; }
        IGroupRepository Group { get; }
        IGroupMemberRepository GroupMember { get; }
        Task<int> Commit();
    }
}
