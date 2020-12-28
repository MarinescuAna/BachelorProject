﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Group.Domain;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork_API.ErrorHandler;
using TeamWork_API.Utils;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        public GroupController(IGroupService group, IUserService user, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(configuration, httpContextAccessor)
        {
            _groupService = group;
            _userService = user;
        }

        [HttpPost]
        [Route("CreateGroupByUser")]
        public async Task<IActionResult> CreateGroupByUser(GroupDetalisReceived detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            var group = await _groupService.GetGroupByNameAsync(detalisReceived.GroupName);
            if (group != null)
            {
                return StatusCode(Codes.Number_404, Conflict409Error.GroupAlreadyExist);
            }

            var teacher = await _userService.GetUserByEmailAsync(detalisReceived.TeacherEmail);
            if (teacher == null)
            {
                return StatusCode(Codes.Number_404, NotFound404Error.InvalidEmail);
            }
            if (teacher.UserRole != Role.TEACHER)
            {
                return StatusCode(Codes.Number_404, NotFound404Error.NotBelongToTeacher);
            }

            var key = await _groupService.CreateGroupByUserAsync(detalisReceived);

            if (key == Guid.Empty)
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            var response = new CreateGroupResponse
            {
                Key = key
            };
            return StatusCode(Codes.Number_201, response);
        }

        [HttpPost]
        [Route("JoinToGroup")]
        public async Task<IActionResult> JoinToGroup(JoinGroup joinGroup)
        {
            if (joinGroup == null || string.IsNullOrEmpty(joinGroup.Key))
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            var group = await _groupService.GetGroupByKeyAsync(joinGroup.Key);
            if (group == null)
            {
                return StatusCode(Codes.Number_404, NotFound404Error.InvalidKey);
            }

            var user = await _userService.GetUserByEmailAsync(joinGroup.AttenderEmail);
            if (await _groupService.GetGroupMemberByKeyIdAsync(joinGroup.Key, user.UserId) != null)
            {
                return StatusCode(Codes.Number_409, Conflict409Error.PartFromGroup);
            }

            if (await _groupService.JoinToGroupAsync(joinGroup) < Codes.Number_1)
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetMyGroups")]
        public async Task<IActionResult> GetMyGroups()
        {
            var userEmail = ExtractEmailFromJWT();

            var user = await _userService.GetUserByEmailAsync(userEmail);

            var groups = await _groupService.GetGroupsAsync(user);

            return StatusCode(Codes.Number_200, groups);
        }
        [HttpGet]
        [Route("GetMembersByGroupKey")]
        public async Task<IActionResult> GetMembersByGroupKey(string key)
        {
            var groups = await _groupService.GetGroupMembersByKeyAsync(key);

            return StatusCode(Codes.Number_200, groups);
        }

        [HttpDelete]
        [Route("LeaveGroup")]
        public async Task<IActionResult> LeaveGroup(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            var email = ExtractEmailFromJWT();
            var user = await _userService.GetUserByEmailAsync(email);

            if (await _groupService.DeleteUserFromGroupAsync(user, Guid.Parse(id)) == false)
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup(GroupUpdateReceived detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            if (!detalisReceived.OldGroupName.Equals(detalisReceived.GroupName))
            {
                var group = await _groupService.GetGroupByNameAsync(detalisReceived.GroupName);
                if (group != null)
                {
                    return StatusCode(Codes.Number_404, Conflict409Error.GroupAlreadyExist);
                }
            }

            if (!(await _groupService.UpdateGroupAsync(detalisReceived)))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("AddMember")]
        public async Task<IActionResult> AddMember(AddMember detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }


            if (await _groupService.IsMemberToGroupAsync(detalisReceived.AttenderEmail, detalisReceived.GroupKey))
            {
                return StatusCode(Codes.Number_404, Conflict409Error.UserIsPartFromGroup);
            }

            if (!(await _groupService.AddMemberByEmailAsync(detalisReceived.AttenderEmail,detalisReceived.GroupKey)))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
    }
}