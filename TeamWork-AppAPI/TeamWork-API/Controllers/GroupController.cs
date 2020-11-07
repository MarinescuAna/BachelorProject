using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Group.Domain;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork_API.Utils;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        public GroupController(IGroupService group, IUserService user, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _groupService = group;
            _userService = user;
        }

        [HttpPost]
        [Route("CreateGroupByUser")]
        public async Task<IActionResult> CreateGroupByUser(GroupDetalisReceived detalisReceived)
        {
            if (detalisReceived == null)
            {
                return StatusCode(Codes.Number_204, Messages.NoContent_204NoContent);
            }

            var group = await _groupService.GetGroupByNameAsync(detalisReceived.GroupName);
            if (group != null)
            {
                return StatusCode(Codes.Number_404, Messages.GroupAlreadyExist_409Conflict);
            }

            var teacher = await _userService.GetUserByEmailAsync(detalisReceived.TeacherEmail);
            if (teacher == null)
            {
                return StatusCode(Codes.Number_404, Messages.InvalidCredentials_4040NotFound);
            }
            if (teacher.UserRole != Role.TEACHER)
            {
                return StatusCode(Codes.Number_404, Messages.NotBelongToTeacher_4040NotFound);
            }

            var key = await _groupService.CreateGroupByUserAsync(detalisReceived);

            if (key == Guid.Empty)
            {
                return StatusCode(Codes.Number_400, Messages.SthWentWrong_400BadRequest);
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
                return StatusCode(Codes.Number_204, Messages.NoContent_204NoContent);
            }

            var group = await _groupService.GetGroupByKeyAsync(joinGroup.Key);
            if (group == null)
            {
                return StatusCode(Codes.Number_404, Messages.InvalidKey_4040NotFound);
            }

            var user = await _userService.GetUserByEmailAsync(joinGroup.AttenderEmail);
            if (await _groupService.GetGroupMemberByKeyIdAsync(joinGroup.Key, user.UserId) != null)
            {
                return StatusCode(Codes.Number_409, Messages.PartFromGroup_409Conflict);
            }

            if (await _groupService.JoinToGroupAsync(joinGroup) < 1)
            {
                return StatusCode(Codes.Number_400, Messages.SthWentWrong_400BadRequest);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetMyGroups")]
        public async Task<IActionResult> GetMyGroups()
        {
            var token = HttpContext.Session.GetString(Constants.Token);
            var tokenTake = new JwtSecurityToken(token);
            var user = await _userService.GetUserByEmailAsync(tokenTake.Claims.FirstOrDefault()?.Value);

            var groups = await _groupService.GetGroups(user);
            return StatusCode(Codes.Number_200,groups);
        }
    }
}