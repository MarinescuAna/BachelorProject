using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class GradeController : ControllerBase
    {
        private readonly IAssignedTaskService _assignedTaskService;
        private readonly IListService _listService;
        private readonly IGroupService _groupService;
        private readonly IPeerEvaluationService _peerEvaluationService;
        private readonly ICheckListGradeService _checkListGradeService;
        public GradeController(
            IPeerEvaluationService peerEvaluationService,
            IAssignedTaskService assignedTaskService,
            IListService listService,
            IGroupService groupService,
            ICheckListGradeService checkListGradeService
            )
        {
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
                var evalPeer = await _peerEvaluationService.GetPeerEvaluationByAssignedTaskIdAndEmailAsync(assignedTask.AssignedTaskID, member.Email);
                var chkgrade = await _checkListGradeService.GetCheckListGradeByUserIdAssignedTaskIDAsync(
                    assignedTask.AssignedTaskID, member.Email);

                list.Add(new DisplayGrades { 
                AssignmentTitle=assignedTask.Assignment.Title,
                GroupName=assignedTask.List.Group.GroupName,
                Fullname=member.FullName,
                GradeTeacher=assignedTask.TeacherGrade.ToString(),
                GradePeerEvaluation= evalPeer == null ? Constants.Minus : evalPeer.Grade.ToString(),
                Comment= evalPeer == null ? Constants.Minus : evalPeer.Comments,
                GradeChecklist= chkgrade==null? Constants.Minus : chkgrade.Grade.ToString()      
                });
            }

            return list;
        }
    }
}
