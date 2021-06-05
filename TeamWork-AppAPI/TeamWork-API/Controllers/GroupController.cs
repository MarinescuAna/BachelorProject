﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.Common.Enums;
using TeamWork.DataAccess.Domain.GroupDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IAssignedTaskService _assignedTaskService;
        public GroupController(IAssignedTaskService assignedTaskService, IGroupService group, IUserService user, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(configuration, httpContextAccessor)
        {
            _groupService = group;
            _userService = user;
            _assignedTaskService = assignedTaskService;
        }

        [HttpPost]
        [Route("CreateGroupByUser")]
        public async Task<IActionResult> CreateGroupByUser(GroupDetalisReceived detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var group = await _groupService.GetGroupByNameAsync(detalisReceived.GroupName);
            if (group != null)
            {
                return StatusCode(Number.Number_409, Conflict409Error.GroupAlreadyExist);
            }

            var teacher = await _userService.GetUserByEmailAsync(detalisReceived.TeacherEmail);
            if (teacher == null)
            {
                return StatusCode(Number.Number_404, NotFound404Error.InvalidEmail);
            }
            if (teacher.UserRole != Role.TEACHER)
            {
                return StatusCode(Number.Number_404, NotFound404Error.NotBelongToTeacher);
            }

            var key = await _groupService.CreateGroupByUserAsync(detalisReceived);

            if (key == Guid.Empty)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return StatusCode(Number.Number_201, new CreateGroupResponse
            {
                Key = key
            });
        }

        [HttpPost]
        [Route("JoinToGroup")]
        public async Task<IActionResult> JoinToGroup(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var group = await _groupService.GetGroupByKeyAsync(key);
            if (group == null)
            {
                return StatusCode(Number.Number_404, NotFound404Error.InvalidKey);
            }

            var userEmail = ExtractEmailFromJWT();
            if (await _groupService.GetGroupMemberByKeyIdAsync(key, userEmail) != null)
            {
                return StatusCode(Number.Number_409, Conflict409Error.PartFromGroup);
            }

            if (!(await _groupService.JoinToGroupAsync(key, userEmail)))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetMyGroups")]
        public async Task<IActionResult> GetMyGroups(int status)
        {
            var userEmail = ExtractEmailFromJWT();

            var statusRequest = status == Number.Number_0 ? StatusRequest.Joined : status == Number.Number_1 ? StatusRequest.Waiting : StatusRequest.Declined;

            var groups = await _groupService.GetGroupsAsync(userEmail, statusRequest);

            return StatusCode(Number.Number_200, groups);
        }

        [HttpGet]
        [Route("GetMembersByGroupKey")]
        public async Task<IActionResult> GetMembersByGroupKey(string key)
        {
            var groups = await _groupService.GetGroupMembersByKeyAsync(Guid.Parse(key));

            return StatusCode(Number.Number_200, groups);
        }
        //TODO testata
        [HttpGet]
        [Route("GetMembersByAssignedTaskIdKey")]
        public async Task<IActionResult> GetMembersByAssignedTaskIdKey(string key)
        {
            var members = new System.Collections.Generic.List<Member>();
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(Number.Number_200, members);
            }

            var assignedTask = await _assignedTaskService.GetAssignedByIdAsync(Guid.Parse(key));
            members = await _groupService.GetGroupMembersByKeyAsync((Guid)assignedTask.List.GroupID);

            return StatusCode(Number.Number_200, members.Where(u => u.Role == Role.STUDENT.ToString()));
        }

        [HttpDelete]
        [Route("LeaveGroup")]
        public async Task<IActionResult> LeaveGroup(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var email = ExtractEmailFromJWT();
            var user = await _userService.GetUserByEmailAsync(email);

            if (await _groupService.DeleteUserFromGroupAsync(user, Guid.Parse(id)) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            if (await _groupService.GetNoMembersFromGroupByGuidAsync(Guid.Parse(id)) == Number.Number_0)
            {
                await _groupService.DeleteGroupAsync(Guid.Parse(id));
            }

            return Ok();
        }

        [HttpPut]
        [Route("AcceptInvitation")]
        public async Task<IActionResult> AcceptInvitation(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (await _groupService.UpdateGroupMemberAsync(key, ExtractEmailFromJWT()) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("GetOutMember")]
        public async Task<IActionResult> GetOutMember(DeleteUserFromGroup user)
        {
            if (user == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var userr = await _userService.GetUserByEmailAsync(user.Email);
            if (await _groupService.DeleteUserFromGroupAsync(userr, Guid.Parse(user.GroupKey)) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            if (await _groupService.GetNoMembersFromGroupByGuidAsync(Guid.Parse(user.GroupKey)) == Number.Number_0)
            {
                await _groupService.DeleteGroupAsync(Guid.Parse(user.GroupKey));
            }

            return Ok();
        }

        [HttpPut]
        [Route("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup(GroupUpdateReceived detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (!detalisReceived.OldGroupName.Equals(detalisReceived.GroupName))
            {
                var group = await _groupService.GetGroupByNameAsync(detalisReceived.GroupName);
                if (group != null)
                {
                    return StatusCode(Number.Number_409, Conflict409Error.GroupAlreadyExist);
                }
            }

            if (!(await _groupService.UpdateGroupAsync(detalisReceived)))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("AddMember")]
        public async Task<IActionResult> AddMember(AddMember detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }


            if (await _groupService.IsMemberToGroupAsync(detalisReceived.AttenderEmail, detalisReceived.GroupKey))
            {
                return StatusCode(Number.Number_409, Conflict409Error.UserIsPartFromGroup);
            }

            if (!await _groupService.AddMemberByEmailAsync(new GroupMember
                {
                StatusRequest = StatusRequest.Waiting,
                UserID = detalisReceived.AttenderEmail,
                GroupID = Guid.Parse(detalisReceived.GroupKey)
            }))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
        [HttpPost]
        [Route("CreateGroupsRandom")]
        public async Task<IActionResult> CreateGroupsRandom(RandomGroupsCreate data)
        {
            if (data == null)
            {
                data.Error = NoContent204Error.NoContent;
                return Ok(data);
            }

            var result = await VerifyMembers(data.Emails);
            if (!string.IsNullOrEmpty(result.Replace(Constants.NewLine, Constants.EmptyString)))
            {
                data.Error = result;
                return Ok(data);
            }

            ShuffleData(ref data);

            data.Error = Constants.EmptyString;
            return Ok(data);
        }
        [HttpPost]
        [Route("SentInvitationsRandom")]
        public async Task<IActionResult> SentInvitationsRandom(RandomGroupsCreate random)
        {
            if (random == null)
            {
                random.Error = NoContent204Error.NoContent;
                return Ok(random);
            }

            var groupsAdded = new Dictionary<string,Guid>();

            for(var index = 0; index < random.Emails.Count && string.IsNullOrEmpty(random.Error); index++)
            {
                if (!groupsAdded.ContainsKey(random.GroupNames[index]))
                {
                    var guid = Guid.NewGuid();
                    groupsAdded.Add(random.GroupNames[index], guid);
                    if(!await _groupService.CreateGroupAsync(new Group { 
                        Description=Constants.ThisGroupWasRandomlyGenerated,
                        GroupName= random.GroupNames[index],
                        GroupUniqueID = guid
                    }))
                    {
                        random.Error = BadRequest400Error.SomethingWentWrong;
                    }
                    if (!await _groupService.AddMemberByEmailAsync(new GroupMember
                    {
                        GroupID=guid,
                        StatusRequest=StatusRequest.Joined,
                        UserID=ExtractEmailFromJWT(),
                        GroupMemberID=Guid.NewGuid()
                    }))
                    {
                        random.Error = BadRequest400Error.SomethingWentWrong;
                    }
                }

                if (!await _groupService.AddMemberByEmailAsync(new GroupMember
                {
                    GroupID = groupsAdded[random.GroupNames[index]],
                    StatusRequest = StatusRequest.Joined,
                    UserID = random.Emails[index],
                    GroupMemberID = Guid.NewGuid()
                }))
                {
                    random.Error = BadRequest400Error.SomethingWentWrong;
                }
            }

            return Ok(random);
        }
        private void ShuffleData(ref RandomGroupsCreate random)
        {
            var indexesUsed = new List<int>();
            var numberAssignedGroups = 0;
            var randomGenerator = new Random();
            var generatedNumber = 0;

            while (numberAssignedGroups != random.Emails.Count)
            {
                var groupName = Constants.Group + Constants.BlankSpace + randomGenerator.Next(Number.Number_1000000);
                for (var index = 0; index < int.Parse(random.NumberMax) && numberAssignedGroups != random.Emails.Count; index++)
                {
                    do
                    {
                        generatedNumber = randomGenerator.Next(random.Emails.Count);
                    } while (indexesUsed.Contains(generatedNumber));

                    random.GroupNames[generatedNumber] = groupName;
                    indexesUsed.Add(generatedNumber);
                    numberAssignedGroups++;
                }
            }
        }
        private async Task<string> VerifyMembers(List<string> list)
        {
            var countMessagesNoAccount = 0;
            var countMessagesTeacherRole = 0;
            var messageNoAccount = new StringBuilder();
            var messageTeacherRole = new StringBuilder();
            foreach (var email in list)
            {
                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    messageNoAccount.Append(Constants.BlankSpace + user.UserEmailId);
                    countMessagesNoAccount++;
                }
                else
                {
                    if (user.UserRole == Role.TEACHER)
                    {
                        countMessagesTeacherRole++;
                        messageTeacherRole.Append(Constants.BlankSpace + user.UserEmailId);
                    }
                }
            }

            if (countMessagesNoAccount > 0)
            {
                messageNoAccount.Append(Constants.NoAccount);
            }
            if (countMessagesTeacherRole > 0)
            {
                if (countMessagesTeacherRole == 1)
                {
                    messageTeacherRole.Append(Constants.IsTeacher);
                }
                else
                {
                    messageTeacherRole.Append(Constants.AreTeachers);
                }
            }

            return messageNoAccount.ToString() +
                Constants.NewLine +
                messageTeacherRole.ToString();
        }
    }
}