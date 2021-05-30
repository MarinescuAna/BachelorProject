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
using TeamWork.DataAccess.Domain.AssignmentDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [HttpGet]
        [Route("GetTasks")]
        public async Task<IActionResult> GetTasks(string listId)
        {
            var listReturn = new List<AssignmentDisplay>();
            if (string.IsNullOrEmpty(listId))
            {
                return StatusCode(Number.Number_200, listReturn);
            }

            var assignments = await _assignmentService.GetAssignmentsByListIdAsync(Guid.Parse(listId));

            foreach (var assignment in assignments)
            {
                listReturn.Add(new AssignmentDisplay
                {
                    Deadline = assignment.Deadline.ToString(),
                    AssignmentId = assignment.AssignmentID.ToString(),
                    ChecklistDeadline = assignment.ChecklistDeadline.ToString(),
                    CreatedDate = assignment.CreatedDate.ToString(),
                    Description = assignment.Description,
                    GroupsMax = assignment.GroupsMax == -1 ? "unset" : assignment.GroupsMax.ToString(),
                    GroupsTake = assignment.GroupsMax == -1 ? "unset" : (assignment.GroupsMax - assignment.GroupsTake).ToString(),
                    Title = assignment.Title,
                    ListID = assignment.ListID.ToString(),
                    Status = DateTime.Compare((DateTime)assignment.Deadline, DateTime.Now) < 0 ? "PASS" :
                        assignment.GroupsTake == assignment.GroupsMax ? "TAKEN" : "ACTIVE"
                });
            }

            return StatusCode(Number.Number_200, listReturn);
        }
        [HttpPost]
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask(CreateAssignment assignment)
        {
            if (assignment == null || string.IsNullOrEmpty(assignment.Title))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (string.IsNullOrEmpty(assignment.Deadline))
            {
                return StatusCode(Number.Number_409, Conflict409Error.DeadlineNotSetedExist);
            }

            var assignmentExist = await _assignmentService.GetAssignmentByAssignmentTitleAsync(assignment.Title);
            if (assignmentExist != null)
            {
                if (assignmentExist?.ListID.ToString() == assignment.ListId)
                {
                    return StatusCode(Number.Number_409, Conflict409Error.AssignmentAlreadyExist);
                }
            }

            if (!await _assignmentService.InsertTaskAsync(new Assignment
            {
                AssignmentID = Guid.NewGuid(),
                ChecklistDeadline = string.IsNullOrEmpty(assignment.ChecklistDeadline) ? DateTime.Parse(assignment.Deadline) : DateTime.Parse(assignment.ChecklistDeadline),
                ListID = Guid.Parse(assignment.ListId),
                Title = assignment.Title,
                GroupsMax = !string.IsNullOrEmpty(assignment.GroupsMax) ? int.Parse(assignment.GroupsMax) : -1,
                Deadline = DateTime.Parse(assignment.Deadline),
                Description = assignment.Description,
                CreatedDate = DateTime.Now,
                GroupsTake = 0,
            }))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }


            return Ok();
        }
        [HttpDelete]
        [Route("DeleteAssignment")]
        public async Task<IActionResult> DeleteAssignment(string assignmentId)
        {
            if (string.IsNullOrEmpty(assignmentId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (await _assignmentService.DeleteAssignmentAsync(Guid.Parse(assignmentId)) == false)
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

    }


}
