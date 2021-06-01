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
        public ListController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IListService listService) : base(configuration, httpContextAccessor)
        {
            _listService = listService;
        }
        //TODO testeaza
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
        /// <summary>
        /// The method will create a list in two different cases:
        /// - first is when the teacher do that which means that the deadline may be set or not, the group id will be null
        /// - second when the group of students to that and this means that the deadline will not be set and also for the email
        /// Important!! 
        /// The title of the list will never be unique.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //TODO testata
        [HttpPost]
        [Route("CreateList")]
        public async Task<IActionResult> CreateList(CreateList list)
        {
            if (list==null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var newList = new List
            {
                Domain = list.Domain,
                ListID = Guid.NewGuid(),
                Title = list.Title,
                UserID = string.IsNullOrEmpty(list.GroupId) ? ExtractEmailFromJWT() : string.Empty
            };

            if (!string.IsNullOrEmpty(list.GroupId))
            {
                newList.GroupID = Guid.Parse(list.GroupId);
            }

            if (!string.IsNullOrEmpty(list.ListDeadline))
            {
                newList.ListDeadline = DateTime.Parse(list.ListDeadline);
            }

            if (!await _listService.InsertListAsync(newList))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        /// <summary>
        /// This method is called by a group with the group id and by the teacher and the filtration will be made
        /// according to his email
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>

        //TODO testata
        [HttpGet]
        [Route("GetLists")]
        public async Task<IActionResult> GetLists(string groupId)
        {
            var lists = string.IsNullOrEmpty(groupId)? 
                await _listService.GetListsByEmailAsync(ExtractEmailFromJWT()):
                await _listService.GetListsByGroupIdAsync(Guid.Parse(groupId));

            var listReturn = new List<DisplayList>();

            foreach(var list in lists.Where(u=>u.IsDeleted==false))
            {
                listReturn.Add(new DisplayList
                {
                    Deadline = list.ListDeadline!=null? 
                            list.ListDeadline.ToString():    
                            "UNSET",
                    Domain = list.Domain,
                    Key = list.ListID.ToString(),
                    Tasks = list.Assignments?.Count.ToString(),
                    Title = list.Title
                }); 
            }
           
            return StatusCode(Number.Number_200,listReturn);
        }
       
    }
}
