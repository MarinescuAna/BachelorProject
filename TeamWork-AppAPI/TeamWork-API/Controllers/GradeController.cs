using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.Enums;
using TeamWork.DataAccess.Domain.AssignmentDTO;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GradeController : BaseController
    {
        private readonly IAssignedTaskService _assignedTaskService;
        private readonly IListService _listService;
        private readonly IGroupService _groupService;
        private readonly IPeerEvaluationService _peerEvaluationService;
        private readonly ICheckListGradeService _checkListGradeService;
        private readonly IAverageService _averageService;
        public GradeController(
            IAverageService averageService,
            IPeerEvaluationService peerEvaluationService,
            IAssignedTaskService assignedTaskService,
            IListService listService,
            IGroupService groupService,
            ICheckListGradeService checkListGradeService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _averageService = averageService;
            _checkListGradeService = checkListGradeService;
            _peerEvaluationService = peerEvaluationService;
            _groupService = groupService;
            _listService = listService;
            _assignedTaskService = assignedTaskService;
        }

        [HttpGet]
        [Route("GetGrades")]
        public async Task<IActionResult> GetGrades(string listId)
        {
            var grades = new List<DisplayGrades>();
            if (string.IsNullOrEmpty(listId))
            {
                return Ok(grades);
            }

            var list = await _listService.GetListByListIdAsync(Guid.Parse(listId));
            foreach(var assignment in list.Assignments)
            {
                foreach(var assignedTask in await _assignedTaskService.GetAssignedTasksByAssignmentIdAsync(assignment.AssignmentID))
                {
                    grades = await GetGroupMembers(grades,assignedTask);
                }
            }

            return Ok(grades);
        }
        private async Task<List<DisplayGrades>> GetGroupMembers(List<DisplayGrades> list,AssignedTask assignedTask)
        {
            var groupMembers = await _groupService.GetGroupMembersByKeyAsync((Guid)assignedTask.List.GroupID);
            foreach(var member in groupMembers.Where(u=>u.Role==Role.STUDENT.ToString()))
            {
                var evalPeer = await _peerEvaluationService.GetCurrentUserGradeByAssignedTaskIdAndEmailAsync(
                    assignedTask.AssignedTaskID, member.Email);
                var chkgrade = await _checkListGradeService.GetCheckListGradeByUserIdAssignedTaskIDAsync(
                    assignedTask.AssignedTaskID, member.Email);
                var average = await _averageService.GetAverageByAssignedTaskIdAndStudentIdAsync
                        (assignedTask.AssignedTaskID, member.Email);

                list.Add(new DisplayGrades { 
                    AssignedTaskId=assignedTask.AssignedTaskID.ToString(),
                    AssignmentTitle=assignedTask.Assignment.Title,
                    GroupName=assignedTask.List.Group.GroupName,
                    Fullname=member.FullName,
                    StudentID=member.Email,
                    GradeTeacher=assignedTask.TeacherGrade.ToString(),
                    GradePeerEvaluation= evalPeer == null ? Constants.Minus : evalPeer.Grade.ToString(),
                    Comment= evalPeer == null ? Constants.Minus : evalPeer.Comments,
                    GradeChecklist= chkgrade==null? Constants.Minus : chkgrade.Grade.ToString()   ,
                    Average = average==null?Constants.Minus:average.GradePerAssignedTask.ToString()
                });
            }

            return list;
        }
        [HttpGet]
        [Route("GetCurrentUserGrades")]
        public async Task<IActionResult> GetCurrentUserGrades(string listId)
        {
            var grades = new List<DisplayGrades>();
            if (string.IsNullOrEmpty(listId))
            {
                return Ok(grades);
            }

            var currentUserEmail = ExtractEmailFromJWT();
            var assignedTasks = await _assignedTaskService.GetAssignedTasksByListIdAsync(Guid.Parse(listId));
            foreach (var assignedTask in assignedTasks)
            {
                var evalPeer = await _peerEvaluationService.GetCurrentUserGradeByAssignedTaskIdAndEmailAsync(
                  assignedTask.AssignedTaskID, currentUserEmail);
                var chkgrade = await _checkListGradeService.GetCheckListGradeByUserIdAssignedTaskIDAsync(
                    assignedTask.AssignedTaskID, currentUserEmail);
                var average = await _averageService.GetAverageByAssignedTaskIdAndStudentIdAsync
                        (assignedTask.AssignedTaskID, currentUserEmail);

                grades.Add(new DisplayGrades
                {
                    AssignedTaskId = assignedTask.AssignedTaskID.ToString(),
                    AssignmentTitle = assignedTask.Assignment.Title,
                    GradeTeacher = assignedTask.TeacherGrade.ToString(),
                    GradePeerEvaluation = evalPeer == null ? Constants.Minus : evalPeer.Grade.ToString(),
                    Comment = evalPeer == null ? Constants.Minus : evalPeer.Comments,
                    GradeChecklist = chkgrade == null ? Constants.Minus : chkgrade.Grade.ToString(),
                    Average = average == null ? Constants.Minus : average.GradePerAssignedTask.ToString()
                });
            }

            return Ok(grades);
        }

    }
}
