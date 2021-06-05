using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.Common.Enums;
using TeamWork.DataAccess.Domain.DashboardDTO;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IAssignedTaskService _assignedTaskService;
        private readonly IListService _listService;
        private readonly IGroupService _groupService;
        private readonly IPeerEvaluationService _peerEvaluationService;
        private readonly ICheckListGradeService _checkListGradeService;
        private readonly ICheckService _checkService;
        private readonly IAverageService _averageService;
        public DashboardController(
            IAverageService averageService,
            IPeerEvaluationService peerEvaluationService,
            IAssignedTaskService assignedTaskService,
            IListService listService,
            IGroupService groupService,
            ICheckListGradeService checkListGradeService,
            ICheckService checkService
            )
        {
            _averageService = averageService;
            _checkListGradeService = checkListGradeService;
            _checkService = checkService;
            _peerEvaluationService = peerEvaluationService;
            _groupService = groupService;
            _listService = listService;
            _assignedTaskService = assignedTaskService;
        }

        [HttpGet]
        [Route("GetReport")]
        public async Task<IActionResult> GetReport(string text)
        {

            if (string.IsNullOrEmpty(text))
            {
                return Ok(new DashboardData
                {
                    Error = NoContent204Error.NoContent
                });
            }

            var dashboard = new List<DashboardData>();
            var assignmentId = Guid.Parse(text.Split(Constants.Asterik)[0]);
            var groupId = Guid.Parse(text.Split(Constants.Asterik)[1]);
            var assignment = await _assignedTaskService.GetAssignedTasksByAssignmentIdAsync(assignmentId);
            var members = await _groupService.GetGroupMembersByKeyAsync(groupId);

            foreach (var assign in assignment.Where(u => u.List.GroupID == groupId))
            {
                foreach (var student in members.Where(u => u.Role != Role.TEACHER.ToString()))
                {
                    var check = await _checkListGradeService.GetCheckListGradeByUserIdAssignedTaskIDAsync(assign.AssignedTaskID,
                            student.Email);
                    var peerEvaluation = await _peerEvaluationService.GetPeerEvaluationByAssignedTaskIdAndEmailAsync(
                        assign.AssignedTaskID, student.Email);
                    var tasks = await _checkService.GetChecksByEmailAssignedTaskIDAsync(assign.AssignedTaskID, student.Email);

                    dashboard.Add(new DashboardData
                    {
                        Fullname = student.FullName,
                        EmailStudent = student.Email,
                        ChecklistEvaluationGrade = check == null ? Constants.Zero : check.Grade.ToString(),
                        PeerEvaluationGrade = peerEvaluation == null ? Constants.Zero : peerEvaluation.Grade.ToString(),
                        Tasks = tasks.Count.ToString(),
                        TasksDone = tasks.Where(u => u.IsChecked == 1).Count().ToString()
                    });
                }
            }
            return Ok(dashboard);
        }
    }
}
