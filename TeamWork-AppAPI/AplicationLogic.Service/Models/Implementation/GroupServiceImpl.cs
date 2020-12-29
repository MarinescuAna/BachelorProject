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
        private readonly IUserService _userService;
        public GroupServiceImpl(IUnitOfWork uow) : base(uow)
        {
            _userService = new UserServiceImpl(uow);
        }
        public async Task<int> GetNoMembersFromGroupByGuidAsync(Guid key) => (await _unitOfWork
           .GroupMember
           .GetItems())
           .Where(s => s.Group.GroupUniqueID == key)
           .Count();
        private async Task<User> GetTeacherEmailByGroupIdAsync(Guid guid)
        {
            try {
                var response = (await _unitOfWork
                   .GroupMember
                   .GetItems())
                   .FirstOrDefault(s => s.Group.GroupUniqueID == guid
                                   && s.User.UserRole == Role.TEACHER)?
                   .User;
                return response;
            }
            catch (Exception ex)
            {
                _unitOfWork.LoggMessageError(Messages.GetTeacherEmailByGroupIdAsync, ex.Message);
                return null; 
            }
         }
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

            var teacher = await _userService.GetUserByEmailAsync(groupDetalis.TeacherEmail);
            var student = await _userService.GetUserByEmailAsync(groupDetalis.StudentCreatorEmail);

            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                StatusRequest = StatusRequest.Waiting,
                Group = group,
                User = teacher
            });

            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                StatusRequest = StatusRequest.Joined,
                User = student,
                Group = group
            });

            if (await _unitOfWork.Commit(Messages.CreateGroupByUserAsync) < 3)
            {
                key = Guid.Empty;
            }

            return key;
        }
        public async Task<int> JoinToGroupAsync(JoinGroup group)
        {
            var user = await _userService.GetUserByEmailAsync(group.AttenderEmail);
            var newMember = new GroupMember
            {
                StatusRequest = StatusRequest.Joined,
                GroupID = Guid.Parse(group.Key),
                UserID = user.UserEmailId
            };

            _unitOfWork.GroupMember.InsertItem(newMember);
            return await _unitOfWork.Commit(Messages.JoinToGroupAsync);
        }
        public async Task<List<ViewGroups>> GetGroupsAsync(string userEmail, StatusRequest status)
        {
            List<ViewGroups> viewGroups = new List<ViewGroups>();
            var groups = (await _unitOfWork.GroupMember.GetItems()).Where(s => s.User.UserEmailId == userEmail && s.StatusRequest==status);

            foreach (var group in groups)
            {
                var teacher = await GetTeacherEmailByGroupIdAsync(group.Group.GroupUniqueID);
                viewGroups.Add(
                    new ViewGroups
                    {
                        GroupName = group.Group.GroupName,
                        NoMembers = group.Group.GroupMembers.Count.ToString(),
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
            await _unitOfWork.Group.UpdateItem(u => u.GroupUniqueID.ToString() == groupDetalis.Id,
                new Group
                {
                    GroupUniqueID = Guid.Parse(groupDetalis.Id),
                    Description = groupDetalis.Description,
                    GroupName = groupDetalis.GroupName
                });
            return await _unitOfWork.Commit(Messages.UpdateGroupAsync) > 0;
        }
        public async Task<bool> AddMemberByEmailAsync(string userEmail, string groupKey)
        {
            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                StatusRequest = StatusRequest.Waiting,
                UserID = (await _userService.GetUserByEmailAsync(userEmail)).UserEmailId,
                GroupID = (await GetGroupByKeyAsync(groupKey)).GroupUniqueID
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
                        UserId = user.UserID.ToString(),
                        Role = user.User?.UserRole.ToString()
                    });
            }

            return viewMembers;
        }
    }
}
