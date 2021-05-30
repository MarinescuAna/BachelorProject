using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.Common.Enums;
using TeamWork.DataAccess.Domain.GroupDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IGroupService
    {
        Task<bool> UpdateGroupMemberAsync(string key, string email);
        Task<int> GetNoMembersFromGroupByGuidAsync(Guid key);
        Task<Guid> CreateGroupByUserAsync(GroupDetalisReceived groupDetalis);
        Task<bool> UpdateGroupAsync(GroupUpdateReceived groupDetalis);
        Task<Group> GetGroupByNameAsync(string name);
        Task<Group> GetGroupByKeyAsync(string key);
        Task<bool> JoinToGroupAsync(string key, string email);
        Task<GroupMember> GetGroupMemberByKeyIdAsync(string key, string email);
        Task<List<ViewGroups>> GetGroupsAsync(string userEmail, StatusRequest status);
        Task<bool> DeleteUserFromGroupAsync(User user, Guid group);
        Task<bool> IsMemberToGroupAsync(string userEmail, string groupKey);
        Task<bool> AddMemberByEmailAsync(string userEmail, string groupKey);
        Task<List<Member>> GetGroupMembersByKeyAsync(Guid key);
        Task<bool> DeleteGroupAsync(Guid group);
    }
}
