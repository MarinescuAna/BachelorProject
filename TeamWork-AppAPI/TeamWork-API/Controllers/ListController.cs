using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.DataAccess.Domain.ListDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ListController : BaseController
    {
        private readonly IListService _listService;
        private readonly IAssignmentService _assignmentService;
        public ListController(IAssignmentService assignmentService ,IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IListService listService) : base(configuration, httpContextAccessor)
        {
            _listService = listService;
            _assignmentService = assignmentService;
        }
        [HttpPut]
        [Route("DeleteList")]
        public async Task<IActionResult> DeleteList(string listId)
        {
            if (string.IsNullOrEmpty(listId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var list = await _listService.GetListByListIdAsync(Guid.Parse(listId));
            list.IsDeleted = true;

            if (!await _listService.UpdateListAsync(list))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPost]
        [Route("CreateList")]
        public async Task<IActionResult> CreateList(CreateList list)
        {
            if (list==null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (!await _listService.InsertListAsync(new List {
                Domain = list.Domain,
                ListDeadline = list.ListDeadline,
                ListID = Guid.NewGuid(),
                Title = list.Title,
                UserID = string.IsNullOrEmpty(list.GroupId) ? ExtractEmailFromJWT() : string.Empty,
                GroupID = list.GroupId,
            }))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetLists")]
        public async Task<IActionResult> GetLists(string groupId)
        {
            var lists = string.IsNullOrEmpty(groupId)? await _listService.GetListsByEmailAsync(ExtractEmailFromJWT()):
                await _listService.GetListsByGroupIdAsync(groupId);
            var listReturn = new List<DisplayList>();

            foreach(var list in lists.Where(u=>u.IsDeleted==false))
            {
                listReturn.Add(new DisplayList
                {
                    Deadline = !string.IsNullOrEmpty(list.ListDeadline) ?
                        list.ListDeadline.Contains(".")? list.ListDeadline.Split(".")[0] : list.ListDeadline.ToString():"UNSET",
                    Domain = list.Domain,
                    Key = list.ListID.ToString(),
                    Tasks = (await _assignmentService.GetAssignmentsByListIdAsync(list.ListID)).Count.ToString(),
                    Title = list.Title
                }); 
            }
           
            return StatusCode(Number.Number_200,listReturn);
        }
       
    }
}
