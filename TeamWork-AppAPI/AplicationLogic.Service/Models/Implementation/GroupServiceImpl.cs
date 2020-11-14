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
        private async Task<int> GetNoMembersFromGroupByGuidAsync(Guid key) => (await _unitOfWork
            .GroupMember
            .GetItems())
            .Where(s => s.Group.GroupUniqueID == key)
            .Count();
        private async Task<string> GetTeacherEmailByGroupIdAsync(Guid guid)
        {
            try {
                var response = (await _unitOfWork
                   .GroupMember
                   .GetItems())
                   .FirstOrDefault(s => s.Group.GroupUniqueID == guid
                                   && s.User.UserRole == Role.TEACHER)?
                   .User?
                   .EmailAddress;
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
        public async Task<GroupMember> GetGroupMemberByKeyIdAsync(string key, int id) => await _unitOfWork
            .GroupMember
            .GetItem(u => u.Group.GroupUniqueID.ToString() == key
                          && u.User.UserId == id);
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
            var groupTarget = await GetGroupByKeyAsync(group.Key);
            var newMember = new GroupMember
            {
                StatusRequest = StatusRequest.Joined,
                Group = groupTarget,
                User = user
            };

            _unitOfWork.GroupMember.InsertItem(newMember);
            return await _unitOfWork.Commit(Messages.JoinToGroupAsync);
        }
        public async Task<List<ViewGroups>> GetGroupsAsync(User user)
        {
            List<ViewGroups> viewGroups = new List<ViewGroups>();
            var groups = (await _unitOfWork.GroupMember.GetItems()).Where(s => s.User.UserId == user.UserId && s.StatusRequest==StatusRequest.Joined);

            foreach (var group in groups)
            {
                viewGroups.Add(
                    new ViewGroups
                    {
                        GroupName = group.Group.GroupName,
                        NoMembers = (await GetNoMembersFromGroupByGuidAsync(group.Group.GroupUniqueID)).ToString(),
                        TeacherName = await GetTeacherEmailByGroupIdAsync(group.Group.GroupUniqueID),
                        UniqueKey = group.Group.GroupUniqueID.ToString()
                    });
            }

            return viewGroups;
        }
        public async Task<bool> DeleteUserFromGroupAsync(User user, Guid group)
        {
            var groupMember = await _unitOfWork.GroupMember.GetItem(u => u.Group.GroupUniqueID == group && u.User.EmailAddress == user.EmailAddress);

            if (groupMember == null)
            {
                return false;
            }

           await _unitOfWork.GroupMember.DeleteItem(u => u.GroupMemberID == groupMember.GroupMemberID);

            return await _unitOfWork.Commit(Messages.DeleteUserFromGroupAsync)>0;
        }
    }
}
