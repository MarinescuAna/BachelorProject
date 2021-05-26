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

        [HttpPost]
        [Route("CreateList")]
        public async Task<IActionResult> CreateList(CreateList list)
        {
            if (list==null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (!await _listService.InsertListAsync(new List { 
                Domain=list.Domain,
                ListDeadline=string.IsNullOrEmpty(list.ListDeadline)? DateTime.Now : DateTime.Parse(list.ListDeadline),
                ListID=Guid.NewGuid(),
                Title=list.Title,
                UserID=ExtractEmailFromJWT()
            }))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetLists")]
        public async Task<IActionResult> GetLists()
        {
            var lists = await _listService.GetListsAsync(ExtractEmailFromJWT());
            var listReturn = new List<DisplayList>();

            foreach(var list in lists)
            {
                listReturn.Add(new DisplayList
                {
                    Deadline = list.ListDeadline.ToString(),
                    Domain = list.Domain,
                    Key = list.ListID.ToString(),
                    Tasks = await _assignmentService.TasksAssingedToListAsync(list.ListID),
                    Title=list.Title
                });
            }
           
            return StatusCode(Number.Number_200,listReturn);
        }
    }
}
