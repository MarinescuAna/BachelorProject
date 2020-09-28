using DataAccess.Domain.Group.Domain;
using DataAccess.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Service.Models.Interface
{
    public interface IGroupService
    {
        Task<Guid> CrateGroupByUserAsync(GroupDetalisReceived groupDetalis);
        Task<Group> GetGroupByNameAsync(string name);
        Task<Group> GetGroupByKeyAsync(string key);
        Task<int> JoinToGroupAsync(JoinGroup group);
        Task<GroupMember> GetGroupMemberByKeyIdAsync(string key, int id);
    }
}
