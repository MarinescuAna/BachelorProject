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
using TeamWork.Common.Enums;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;
        private readonly IGroupService _groupService;
        public NotificationController(
            IGroupService groupService,
            INotificationService notificationService,
            IHttpContextAccessor httpContextAccessor)
            :base(httpContextAccessor)
        {
            _groupService = groupService;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("GetNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications =( await _notificationService.GetNotificationsByEmailAsync(ExtractEmailFromJWT()))
                .OrderByDescending(u=>u.CreationDate);

            return Ok(notifications);
        }

        [HttpGet]
        [Route("GetNotificationsNumber")]
        public async Task<IActionResult> GetNotificationsGetNotificationsNumber()
        {
            var notifications = await _notificationService.GetNotificationsByEmailAsync(ExtractEmailFromJWT());
            var groups = await _groupService.GetGroupsAsync(ExtractEmailFromJWT(), StatusRequest.Waiting);

            return Ok(notifications.Count()+groups.Count());
        }

        [HttpDelete]
        [Route("MarkAsSeen")]
        public async Task<IActionResult> MarkAsSeen(string notificationId)
        {
            if (string.IsNullOrEmpty(notificationId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }
            
            if (!await _notificationService.DeleteNotificationAsync(Guid.Parse(notificationId)))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
    }
}
