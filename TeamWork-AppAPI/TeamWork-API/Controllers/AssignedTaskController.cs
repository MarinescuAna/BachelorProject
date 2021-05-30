using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.DataAccess.Domain.AssignedTaskDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignedTaskController : ControllerBase
    {
        private readonly IAssignedTaskService _assignedTaskService;
        private readonly IAssignmentService _assignmentService;
        public AssignedTaskController(
            IAssignedTaskService assignedTaskService, 
            IAssignmentService assignmentService)
        {
            _assignedTaskService = assignedTaskService;
            _assignmentService = assignmentService;
        }

        //TODO testata
        [HttpGet]
        [Route("GetTasksGroup")]
        public async Task<IActionResult> GetTasksGroup(string listId)
        {
            var listReturn = new List<AssignedTaskDisplay>();
            if (string.IsNullOrEmpty(listId))
            {
                return StatusCode(Number.Number_200, listReturn);
            }

            var assignedTasks = await _assignedTaskService.GetAssignedTasksByListIdAsync(Guid.Parse(listId));
            foreach (var assignedTask in assignedTasks)
            {
                listReturn.Add(new AssignedTaskDisplay
                {
                    AssignedTaskId=assignedTask.AssignedTaskID.ToString(),
                    Deadline = assignedTask.Assignment?.Deadline.ToString(),
                    AssignmentId = assignedTask.AssignmentID.ToString(),
                    ChecklistDeadline = assignedTask.Assignment?.ChecklistDeadline.ToString(),
                    Description = assignedTask.Assignment?.Description,
                    Title = assignedTask.Assignment?.Title,
                    ListID=assignedTask.Assignment?.ListID.ToString(),
                    TeacherGrade=assignedTask.TeacherGrade.ToString(),
                    SolutionLink=assignedTask.SolutionLink,
                    StatusTask= string.IsNullOrEmpty(assignedTask.SolutionLink) ?
                        DateTime.Compare((DateTime)(assignedTask.Assignment?.Deadline), DateTime.Now) > 0 ? "ACTIVE" : "PASS" :
                        "DONE",
                    StatusChecklist= string.IsNullOrEmpty(assignedTask.Assignment?.ChecklistDeadline.ToString())?"ACTIVE":
                        DateTime.Compare((DateTime)(assignedTask.Assignment?.ChecklistDeadline), DateTime.Now)>0?"ACTIVE":"PASS"
                });
            }

            return StatusCode(Number.Number_200, listReturn);
        }

        [HttpGet]
        [Route("GetTasksPerGroup")]
        public async Task<IActionResult> GetTasksPerGroup(string listId)
        {
            var listReturn = new List<AssignedTasksPerGroup>();
            if (string.IsNullOrEmpty(listId))
            {
                return StatusCode(Number.Number_200, listReturn);
            }

            var assignedTasks = await _assignedTaskService.GetAssignedTasksPerGroupsByListIdAsync(Guid.Parse(listId));
            foreach (var assignedTask in assignedTasks)
            {
                listReturn.Add(new AssignedTasksPerGroup
                {
                    GroupId=assignedTask.List.GroupID.ToString(),
                    AssignedTaskId = assignedTask.AssignedTaskID.ToString(),
                    AssignmentDeadline = assignedTask.Assignment.Deadline.ToString(),
                    ChkListDeadline = assignedTask.Assignment.ChecklistDeadline.ToString(),
                    AssignmentTitle = assignedTask.Assignment.Title,
                    Grade = assignedTask.TeacherGrade.ToString(),
                    Solution = assignedTask.SolutionLink,
                    GroupName = assignedTask.List.Group.GroupName,
                    Status = string.IsNullOrEmpty(assignedTask.SolutionLink) ?
                        DateTime.Compare((DateTime)assignedTask.Assignment.Deadline, DateTime.Now) > 0 ? "ACTIVE" : "PASS" :
                        "DONE"
                }) ;
            }

            return StatusCode(Number.Number_200, listReturn);
        }

        //TODO testat
        [HttpPost]
        [Route("AssignTask")]
        public async Task<IActionResult> AssignTask(AssignTask assignment)
        {
            if (assignment == null || string.IsNullOrEmpty(assignment.AssignmentId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var assignedTask= new AssignedTask
            {
                AssignedTaskID = Guid.NewGuid(),
                AssignmentID = Guid.Parse(assignment.AssignmentId),
                ListID = Guid.Parse(assignment.ListId)
            };
            if (!await _assignedTaskService.AssignTaskAsync(assignedTask))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            var assignmentTake = await _assignmentService.GetAssignmentByAssignmentIdAsync(Guid.Parse(assignment.AssignmentId));
            assignmentTake.GroupsTake++;
            assignmentTake.List = null;            

            if(!await _assignmentService.UpdateAssignmentAsync(assignmentTake)){
            
                await _assignedTaskService.DeleteTaskAssignedByIdAsync(assignedTask.AssignedTaskID);
                
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpPut]
        [Route("UpdateAssignedTask")]
        public async Task<IActionResult> UpdateAssignedTask(UpdateAssignedTask assignment)
        {
            if (assignment == null || string.IsNullOrEmpty(assignment.AssignedTaskId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }
            var assignmentTemp = await _assignedTaskService.GetAssignedByIdAsync(Guid.Parse(assignment.AssignedTaskId));
            if (assignmentTemp==null)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            assignmentTemp.SolutionLink = string.IsNullOrEmpty(assignment.SolutionLink) ? assignmentTemp.SolutionLink : assignment.SolutionLink;
            assignmentTemp.TeacherGrade = string.IsNullOrEmpty(assignment.TeacherGrade) ? assignmentTemp.TeacherGrade : float.Parse(assignment.TeacherGrade);
            
            if (!await _assignedTaskService.UpdateTaskAsync(assignmentTemp))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
    }
}
