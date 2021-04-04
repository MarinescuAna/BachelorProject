using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
    }
}
