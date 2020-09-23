using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicationLogic.Repository.Models.Interface;
using AplicationLogic.Service.Models.Interface;
using DataAccess.Domain.Group.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TeamWork_API.Utils;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public GroupController(IGroupService group, IUserService user)
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
                return StatusCode(204, Messages.NoContent_204NoContent);
            }

            if (await _userService.GetUserByEmail(detalisReceived.TeacherEmail) == null)
            {
                return StatusCode(404, Messages.InvalidCredentials_4040NotFound);
            }

            var key = await _groupService.CrateGroupByUser(detalisReceived);

            if (key == Guid.Empty)
            {
                return StatusCode(400, Messages.SthWentWrong_400BadRequest);
            }

            return StatusCode(201, new JObject
            {
                 "key", key
            });
        }
    }
}