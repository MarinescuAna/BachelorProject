using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.ApplicationLogic.Service.Utils;
using TeamWork.DataAccess.Domain.Group.Domain;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class GroupServiceImpl : ABaseService, IGroupService
    {
        public GroupServiceImpl(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<int> GetNoMembersFromGroupByGuidAsync(Guid key) => (await _unitOfWork
           .GroupMember
           .GetItems())
           .Where(s => s.Group.GroupUniqueID == key && s.StatusRequest==StatusRequest.Joined)
           .Count();
        public async Task<Group> GetGroupByKeyAsync(string key) => await _unitOfWork
            .Group
            .GetItem(u => u.GroupUniqueID.ToString() == key);
        public async Task<GroupMember> GetGroupMemberByKeyIdAsync(string key, string email) => await _unitOfWork
            .GroupMember
            .GetItem(u => u.Group.GroupUniqueID.ToString() == key
                          && u.User.UserEmailId == email);
        public async Task<Group> GetGroupByNameAsync(string name) => await _unitOfWork
            .Group
            .GetItem(u => u.GroupName == name);       
        public async Task<Guid> CreateGroupByUserAsync(GroupDetalisReceived groupDetalis)
        {
            var key = Guid.NewGuid();
            var group = new Group
            {
                Description = groupDetalis.Description,
                GroupName = groupDetalis.GroupName,
                GroupUniqueID = key
            };

            _unitOfWork.Group.InsertItem(group);

            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                StatusRequest = StatusRequest.Waiting,
                GroupID = key,
                GroupMemberID=Guid.NewGuid(),
                UserID = groupDetalis.TeacherEmail
            });

            if (groupDetalis.TeacherEmail != groupDetalis.StudentCreatorEmail)
            {
                _unitOfWork.GroupMember.InsertItem(new GroupMember
                {
                    StatusRequest = StatusRequest.Joined,
                    GroupID = key,
                    GroupMemberID = Guid.NewGuid(),
                    UserID = groupDetalis.StudentCreatorEmail
                });
            }

            if (await _unitOfWork.Commit(Messages.CreateGroupByUserAsync) < 3)
            {
                key = Guid.Empty;
            }

            return key;
        }
        public async Task<bool> JoinToGroupAsync(string key, string email)
        {
            var newMember = new GroupMember
            {
                StatusRequest = StatusRequest.Joined,
                GroupID = Guid.Parse(key),
                UserID = email
            };

            _unitOfWork.GroupMember.InsertItem(newMember);
            return (await _unitOfWork.Commit(Messages.JoinToGroupAsync))>0;
        }
        public async Task<List<ViewGroups>> GetGroupsAsync(string userEmail, StatusRequest status)
        {
            var viewGroups = new List<ViewGroups>();
            var groups = (await _unitOfWork.GroupMember.GetItems()).Where(s => s.User.UserEmailId == userEmail && s.StatusRequest==status);

            foreach (var group in groups)
            {
                var teacher = group.Group?.GroupMembers?.FirstOrDefault(s=>s.User.UserRole==Role.TEACHER && s.StatusRequest==StatusRequest.Joined)?.User;
                viewGroups.Add(
                    new ViewGroups
                    {
                        GroupName = group.Group.GroupName,
                        NoMembers = group.Group.GroupMembers.Where(s=>s.StatusRequest==StatusRequest.Joined).Count().ToString(),
                        TeacherName = teacher?.FirstName+" "+teacher?.LastName,
                        UniqueKey = group.Group.GroupUniqueID.ToString(),
                        GroupDetails=group.Group.Description,
                        TeacherEmail=teacher?.UserEmailId
                    });
            }

            return viewGroups;
        }
        public async Task<bool> DeleteGroupAsync(Guid group)
        {
            await _unitOfWork.Group.DeleteItem(u => u.GroupUniqueID == group);

            return await _unitOfWork.Commit(Messages.DeleteGroupAsync) > 0;
        }
        public async Task<bool> DeleteUserFromGroupAsync(User user, Guid group)
        {
            var groupMember = await _unitOfWork.GroupMember.GetItem(u => u.Group.GroupUniqueID == group && u.User.UserEmailId == user.UserEmailId);

            if (groupMember == null)
            {
                return false;
            }

           await _unitOfWork.GroupMember.DeleteItem(u => u.GroupMemberID == groupMember.GroupMemberID);

            return await _unitOfWork.Commit(Messages.DeleteUserFromGroupAsync)>0;
        }
        public async Task<bool> IsMemberToGroupAsync(string userEmail, string groupKey)
        {
            return await _unitOfWork.GroupMember.GetItem(u => u.User.UserEmailId == userEmail && u.GroupID.ToString() == groupKey)!=null;
        }
        public async Task<bool> UpdateGroupAsync(GroupUpdateReceived groupDetalis)
        {
            await _unitOfWork.Group.UpdateItem(
                new Group
                {
                    GroupUniqueID = Guid.Parse(groupDetalis.Id),
                    Description = groupDetalis.Description,
                    GroupName = groupDetalis.GroupName
                });
            return await _unitOfWork.Commit(Messages.UpdateGroupAsync) > 0;
        }
        public async Task<bool> UpdateGroupMemberAsync(string key, string email)
        {
            var groupMember =await _unitOfWork.GroupMember.GetItem(u => u.GroupID.ToString() == key && u.UserID == email);
            
            groupMember.StatusRequest = StatusRequest.Joined;

            await _unitOfWork.GroupMember.UpdateItem(groupMember);

            return await _unitOfWork.Commit(Messages.UpdateGroupMemberAsync) > 0;
        }
        public async Task<bool> AddMemberByEmailAsync(string userEmail, string groupKey)
        {
            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                StatusRequest = StatusRequest.Waiting,
                UserID = userEmail,
                GroupID = Guid.Parse(groupKey)
            });

            return await _unitOfWork.Commit(Messages.AddMemberByEmailAsync)>0;
        }
        public async Task<List<Member>> GetGroupMembersByKeyAsync(string key)
        {
            var viewMembers = new List<Member>();
            var users = (await _unitOfWork.GroupMember.GetItems()).
                Where(s => s.GroupID.ToString() == key && s.StatusRequest==StatusRequest.Joined);

            foreach (var user in users)
            {
                viewMembers.Add(
                    new Member
                    {
                        Email = user.User?.UserEmailId,
                        FullName = $"{user.User?.FirstName} {user.User?.LastName}",
                        Institution = user.User?.Institution,
                        Role = user.User?.UserRole.ToString()
                    });
            }

            return viewMembers;
        }
    }
}
