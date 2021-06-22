using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.DataAccess.Domain.AverageDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AverageController : BaseController
    {
        private readonly IAverageService _averageService;
        private readonly INotificationService _notificationService;
        public AverageController(
            INotificationService notificationService,
            IAverageService averageService,
            IHttpContextAccessor httpContextAccessor) : base( httpContextAccessor)
        {
            _notificationService = notificationService;
            _averageService = averageService;
        }

        [HttpPost]
        [Route("ComputeAverage")]
        public async Task<IActionResult> ComputeAverage(AverageInsert[] list)
        {
            if (list == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var teacherEmail = ExtractEmailFromJWT();
            foreach (var average in list)
            {
                if (await _averageService.GetAverageByAssignedTaskIdAndStudentIdAsync(
                    Guid.Parse(average.AssignedTaskID), average.StudentID) == null)
                {
                    if (!await _averageService.InsertAverageAsync(new Average
                    {
                        AssignedTaskID = Guid.Parse(average.AssignedTaskID),
                        GradePerAssignedTask = float.Parse(average.Grade),
                        Id = Guid.NewGuid(),
                        StudentID = average.StudentID,
                        TeacherID = teacherEmail
                    }))
                    {
                        return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
                    }
                    await _notificationService.InsertNotificationAsync(new Notification
                    {
                        ID = Guid.NewGuid(),
                        Message = string.Format(Constants.ReturnedGradeForGroups,teacherEmail,Constants.Media),
                        UserID=average.StudentID,
                        CreationDate=DateTime.Now
                    });
                }
            }


            return Ok();
        }
    }
}
