using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.Common.Enums;
using TeamWork.DataAccess.Domain.CheckDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChecklistGradeController : BaseController
    {
        private readonly ICheckService _checkService;
        private readonly IAssignedTaskService _assignedTaskService;
        private readonly ICheckListGradeService _checkListGradeService;
        private readonly IGroupService _groupService;
        private readonly INotificationService _notificationService;

        public ChecklistGradeController(
            IGroupService groupService,
            IAssignedTaskService assignedTaskService,
            ICheckListGradeService checkListGradeService,
            ICheckService checkService,
            INotificationService notificationService,
            IHttpContextAccessor httpContextAccessor
            ):
            base(httpContextAccessor)
        {
            _notificationService = notificationService;
            _groupService = groupService;
            _assignedTaskService = assignedTaskService;
            _checkListGradeService = checkListGradeService;
            _checkService = checkService;
        }

        //TODO testeaza notificarea
        /// <summary>
        /// This methode will do the follow operations:
        ///  - first it will found all the assigned tasks that have the given assignmentid
        ///  - it will take all this values and found the members that has this task assigned
        ///  - for each member that is not a teacher, will take all the checks created for that assignment and will 
        ///  compute the grade as follow: the number of (checks done* 100)/total check 
        ///  - will store into database the grades for each student and assigned task id
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReturnCheckListGrade")]
        public async Task<IActionResult> ReturnCheckListGrade(string assignmentId)
        {
            if (string.IsNullOrEmpty(assignmentId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }
            var grades = new List<CheckListGrade>();
            var assignedTasks = await _assignedTaskService.GetAssignedTasksByAssignmentIdAsync(Guid.Parse(assignmentId));

            foreach (var assignedTask in assignedTasks)
            {
                var members = await _groupService.GetGroupMembersByKeyAsync((Guid)assignedTask.List.GroupID);
                foreach (var member in members.Where(u => u.Role == Role.STUDENT.ToString()))
                {
                    var result = _checkService.ReportChecksByUserIdAssignedTaskIdAsync(assignedTask.AssignedTaskID, member.Email).Result;
                    grades.Add(new CheckListGrade
                    {
                        AssignedTaskID = assignedTask.AssignedTaskID,
                        Grade = result == (Number.Number_0, Number.Number_0) ? 
                            Number.Number_0 : 
                            (result.tasksDone * Number.Number_100) / result.totalTasks,
                        ID = Guid.NewGuid(),
                        UserID = member.Email
                    });
                }
            }

            foreach (var grade in grades)
            {
                if (!await _checkListGradeService.InsertCheckListGradeAsync(grade))
                {
                    return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
                }
                await _notificationService.InsertNotificationAsync(new Notification { 
                    ID=Guid.NewGuid(),
                    Message= string.Format(Constants.ChecklistGradesReturned,ExtractEmailFromJWT()),
                    UserID=grade.UserID
                });
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetChecklistGrade")]
        public async Task<IActionResult> GetChecklistGrade(string takeGradeChecklist)
        {
            if (string.IsNullOrEmpty(takeGradeChecklist))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var assignedTaskId = Guid.Parse(takeGradeChecklist.Split(Constants.Asterik)[Number.Position_0]);
            var email = takeGradeChecklist.Split(Constants.Asterik)[Number.Position_1];

            var grade = await _checkListGradeService.GetCheckListGradeByUserIdAssignedTaskIDAsync(assignedTaskId, email);
            if (grade == null)
            {
                return Ok();
            }

            return StatusCode(Number.Number_200, grade.Grade.ToString());
        }

    }
}
