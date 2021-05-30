using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.DataAccess.Domain.CheckDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckController : BaseController
    {
        private readonly ICheckService _checkService;
        public CheckController(IConfiguration configuration, ICheckService checkService, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
            _checkService = checkService;
        }

        [HttpGet]
        [Route("GetChecks")]
        public async Task<IActionResult> GetChecks(string text)
        {
            var checks = new List<CheckDisplay>();
            var assignedTaskId = Guid.Parse(text.Split("*")[0]);
            var email = text.Split("*")[1];

            if(string.IsNullOrEmpty(text))
            {
                return StatusCode(Number.Number_200, checks);
            }
            var lists = await _checkService.GetChecksByEmailAssignedTaskIDAsync(assignedTaskId,email);

            foreach (var list in lists)
            {
                checks.Add(new CheckDisplay
                {
                    CheckID = list.CheckID.ToString(),
                    CreationDate = list.CreationDate.ToString(),
                    Description = list.Description,
                    IsChecked =list.IsChecked.ToString(),
                    LastUpdate=list.LastUpdate.ToString(),
                    CreateBy=list.CreateBy
                });
            }

            return StatusCode(Number.Number_200, checks);
        }

        [HttpPost]
        [Route("CreateCheck")]
        public async Task<IActionResult> CreateCheck(InsertCheck check)
        {
            if (check == null || string.IsNullOrEmpty(check.AssignedTaskId) || string.IsNullOrEmpty(check.Email))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (!await _checkService.InsertCheckAsync(new Check
            {
                AssignedTaskID=Guid.Parse(check.AssignedTaskId),
                CheckID=Guid.NewGuid(),
                CreationDate=DateTime.Now,
                Description=check.Description,
                IsChecked=0,
                LastUpdate=DateTime.Now,
                UserId=check.Email,
                CreateBy=check.CreateBy
            }))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }


            return Ok();
        }

        [HttpDelete]
        [Route("DeleteCheck")]
        public async Task<IActionResult> DeleteCheck(string checkId)
        {
            if (string.IsNullOrEmpty(checkId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (await _checkService.DeleteCheckAsync(Guid.Parse(checkId)) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("UpdateCheck")]
        public async Task<IActionResult> AcceptInvitation(UpdateCheck check)
        {
            if (check==null || string.IsNullOrEmpty(check.CheckID))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var oldCheck = await _checkService.GetCheckByCheckIdAsync(Guid.Parse(check.CheckID));
            oldCheck.Description = check.Description;
            oldCheck.LastUpdate = DateTime.Now;

            if (await _checkService.UpdateCheckAsync(oldCheck) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Check")]
        public async Task<IActionResult> Check(string checkId)
        {
            if (string.IsNullOrEmpty(checkId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var oldCheck = await _checkService.GetCheckByCheckIdAsync(Guid.Parse(checkId));
            oldCheck.IsChecked = oldCheck.IsChecked==1?0:1;
            oldCheck.LastUpdate = DateTime.Now;

            if (await _checkService.UpdateCheckAsync(oldCheck) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
    }
}
