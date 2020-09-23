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
    public class GroupServiceImpl:ABaseService, IGroupService
    {
        private IUserService _userService;
        public GroupServiceImpl(IUnitOfWork uow):base(uow)
        {
            _userService = new UserServiceImpl(uow);
        }

        public async Task<Guid> CrateGroupByUser(GroupDetalisReceived groupDetalis)
        {
            var key = Guid.NewGuid();
            var group = new Group
            {
                Description = groupDetalis.Description,
                GroupName = groupDetalis.GroupName,
                GroupUniqueID = key
            };

            _unitOfWork.Group.InsertItem(group);

            var teacher = await _userService.GetUserByEmail(groupDetalis.TeacherEmail);
            var student = await _userService.GetUserByEmail(groupDetalis.StudentCreatorEmail);

            _unitOfWork.GroupMember.InsertItem(new GroupMember { 
            Group=group,
            StatusRequest=StatusRequest.Waiting,
            User=teacher
            });

            _unitOfWork.GroupMember.InsertItem(new GroupMember
            {
                Group = group,
                StatusRequest = StatusRequest.Joined,
                User = student
            });

            if ( await _unitOfWork.Commit() < 3)
            {
                key = Guid.Empty;
            }

            return key;
        }
    }
}
