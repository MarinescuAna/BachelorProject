using AplicationLogic.Repository.UOW;
using AplicationLogic.Service.Models.Interface;
using DataAccess.Domain.Group.Domain;
using DataAccess.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Service.Models.Implementation
{
    public class GroupServiceImpl : ABaseService, IGroupService
    {
        private readonly IUserService _userService;
        public GroupServiceImpl(IUnitOfWork uow) : base(uow)
        {
            _userService = new UserServiceImpl(uow);
        }

        public async Task<Group> GetGroupByKeyAsync(string key)
        {
            return await _unitOfWork.Group.GetItem(u => u.GroupUniqueID.ToString() == key);
        }
        public async Task<GroupMember> GetGroupMemberByKeyIdAsync(string key, int id)
        {
            return await _unitOfWork.GroupMember.GetItem(u => u.GroupUniqueID.ToString() == key && u.UserID==id);
        }
        public async Task<Group> GetGroupByNameAsync(string name)
        {
            return await _unitOfWork.Group.GetItem(u => u.GroupName == name);
        }
        public async Task<Guid> CrateGroupByUserAsync(GroupDetalisReceived groupDetalis)
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
                UserID = teacher.UserId,
                GroupUniqueID=group.GroupUniqueID,
                StatusRequest = StatusRequest.Waiting
            });

            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                GroupUniqueID = group.GroupUniqueID,
                StatusRequest = StatusRequest.Joined,
                UserID = student.UserId
            });

            if (await _unitOfWork.Commit() < 3)
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
                GroupUniqueID=groupTarget.GroupUniqueID,
                StatusRequest=StatusRequest.Joined,
                UserID=user.UserId,
            };

            _unitOfWork.GroupMember.InsertItem(newMember);
            return await _unitOfWork.Commit();
        }
    }
}
