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
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.ConstantStrings.ErrorHandler;
using TeamWork.Common.Enums;
using TeamWork.DataAccess.Domain.GroupDTO;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Domain.PeerEvaluationDTO;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeerEvaluationController : BaseController
    {
        private readonly IPeerEvaluationService _peerEvaluationService;
        private readonly IGroupService _groupService;
        private readonly INotificationService _notificationService;
        public PeerEvaluationController(
            INotificationService notificationService,
            IPeerEvaluationService peerEvaluationService,
            IHttpContextAccessor httpContextAccessor,
            IGroupService groupService)
            : base(httpContextAccessor)
        {
            _notificationService = notificationService;
            _groupService = groupService;
            _peerEvaluationService = peerEvaluationService;
        }
        [HttpGet]
        [Route("GetGrade")]
        public async Task<IActionResult> GetGrade(string assignedTaskId)
        {
            if (string.IsNullOrEmpty(assignedTaskId))
            {
                return StatusCode(Number.Number_204,NoContent204Error.NoContent);
            }

            var peerEvaluationResultForCurrentUser = 
                await _peerEvaluationService.GetCurrentUserGradeByAssignedTaskIdAndEmailAsync(
                    Guid.Parse(assignedTaskId), 
                    ExtractEmailFromJWT());

            if (string.IsNullOrEmpty(peerEvaluationResultForCurrentUser?.Comments))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoEvaluation);
            }

            return Ok(new DisplayPeerEvaluation { 
                Comment=peerEvaluationResultForCurrentUser.Comments,
                Grade=peerEvaluationResultForCurrentUser.Grade.ToString()
            });

        }

        /// <summary>
        /// First thing first is to check the number of members because if they are just 2 (teacher and student)
        /// the evaluation can't be done.
        /// Then we have to check if the shuffle was already made for this assigned task id, because this is done just one
        /// time.
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMemberForEvaluation")]
        public async Task<IActionResult> GetMemberForEvaluation(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Ok(new PeerEvaluationResult
                {
                    Error = NoContent204Error.NoContent
                });
            }

            //split the input data which have the follow format: assignedTaskId*groupId
            var assignedTaskId = Guid.Parse(text.Split(Constants.Asterik)[Number.Position_0]);
            var groupId = Guid.Parse(text.Split(Constants.Asterik)[Number.Position_1]);

            //retrieve from database and check if the evaluation was started by another user and the data 
            //was already shuffle, cause in this case we will return the person that the current user have to
            //evaluate
            var peerEvaluationResultForCurrentUser =
                await _peerEvaluationService.GetPeerEvaluationByAssignedTaskIdAndEmailAsync(
                    assignedTaskId,
                    ExtractEmailFromJWT());

            if (peerEvaluationResultForCurrentUser != null)
            {
                if (!string.IsNullOrEmpty(peerEvaluationResultForCurrentUser.Comments))
                {
                    return Ok(new PeerEvaluationResult { 
                        Error=Conflict409Error.EvaluationPeerAlreadyDone
                    });
                }
                return Ok(new PeerEvaluationResult
                {
                    Error = Constants.EmptyString,
                    Id = peerEvaluationResultForCurrentUser.ID.ToString(),
                    EvaluatingStudentEmail = peerEvaluationResultForCurrentUser.EvaluatedUser.UserEmailId,
                    EvaluatingStudentFullname = peerEvaluationResultForCurrentUser.EvaluatedUser.FirstName +
                        Constants.BlankSpace +
                        peerEvaluationResultForCurrentUser.EvaluatedUser.LastName

                });
            }

            //retreive from database the group which contains the members
            var groupMembsers = await _groupService.GetGroupMembersByKeyAsync(groupId);

            //test to see if the evaluation is possible or not
            if (groupMembsers.Count <= Number.Number_2)
            {
                return Ok(new PeerEvaluationResult
                {
                    Error = Conflict409Error.EvaluationNotPossible
                });
            }

            foreach (var member in groupMembsers)
            {
                await _notificationService.InsertNotificationAsync(new Notification
                {
                    CreationDate = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Message = string.Format(Constants.PeerEvaluationStart, ExtractEmailFromJWT()),
                    UserID=member.Email
                }); 
            }

            //start the shuffle process
            return Ok(await ShuffleMembers(
                    groupMembsers.Where(u => u.Role != Role.TEACHER.ToString()).ToList(),
                    assignedTaskId
                ));
        }

        [HttpPut]
        [Route("AssignPeerEvaluation")]
        public async Task<IActionResult> AssignPeerEvaluation(UpdatePeerEvaluation updatePeerEvaluation)
        {
            if (updatePeerEvaluation == null || string.IsNullOrEmpty(updatePeerEvaluation.PeerEvaluationId))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var peerEvaluation = await _peerEvaluationService.GetPeerEvaluationByIdAsync(Guid.Parse(updatePeerEvaluation.PeerEvaluationId));
            peerEvaluation.Comments = updatePeerEvaluation.Comments;
            peerEvaluation.Grade = float.Parse(updatePeerEvaluation.Grade);
            peerEvaluation.AssignedTask = null;
            peerEvaluation.EvaluatedUser = null;

            if (!await _peerEvaluationService.UpdatePeerEvaluationAsync(peerEvaluation))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            await _notificationService.InsertNotificationAsync(new Notification
            {
                CreationDate = DateTime.Now,
                ID = Guid.NewGuid(),
                Message = Constants.PeerEvaluationReceive,
                UserID = peerEvaluation.UserID
            });

            return Ok();
        }

        private async Task<PeerEvaluationResult> ShuffleMembers(List<Member> groupMembers, Guid assignedTaskId)
        {
            var listPeerEvaluations = new List<PeerEvaluation>();

            if (groupMembers.Count == Number.Number_2)
            {
                listPeerEvaluations.Add(new PeerEvaluation
                {
                    ID = Guid.NewGuid(),
                    AssignedTaskID = assignedTaskId,
                    EvaluatingUserEmail = groupMembers[Number.Number_0].Email,
                    UserID = groupMembers[Number.Number_1].Email
                });
                listPeerEvaluations.Add(new PeerEvaluation
                {
                    ID = Guid.NewGuid(),
                    AssignedTaskID = assignedTaskId,
                    EvaluatingUserEmail = groupMembers[Number.Number_1].Email,
                    UserID = groupMembers[Number.Number_0].Email
                });
            }
            else
            {
                listPeerEvaluations = ShuffleMoreThanTwoUsers(groupMembers, assignedTaskId);
            }

            if (!await InsertListOfPeerEvaluations(listPeerEvaluations))
            {
                return new PeerEvaluationResult
                {
                    Error = BadRequest400Error.SomethingWentWrongInsert
                };
            }

            var evaluatedMember = listPeerEvaluations.First(
                         u => u.EvaluatingUserEmail == ExtractEmailFromJWT()
                     );
            var evaluatedMemberUser = groupMembers.First(u => u.Email == evaluatedMember.UserID);

            return new PeerEvaluationResult
            {
                Id = evaluatedMember.ID.ToString(),
                EvaluatingStudentEmail = evaluatedMemberUser.Email,
                EvaluatingStudentFullname = evaluatedMemberUser.FullName,
                Error=Constants.EmptyString
            };
        }

        private async Task<bool> InsertListOfPeerEvaluations(List<PeerEvaluation> peerEvaluations)
        {
            foreach (var peerEvaluation in peerEvaluations)
            {
                if (!await _peerEvaluationService.InsertPeerEvaluationAsync(peerEvaluation))
                {
                    return false;
                }
            }

            return true;
        }

        private List<PeerEvaluation> ShuffleMoreThanTwoUsers(List<Member> groupMembers, Guid assignedTaskId)
        {
            var listPeerEvaluations = new List<PeerEvaluation>();
            var frequency = new int[groupMembers.Count];
            var random = new Random();
            var randomIndex = Number.Number_1_Negative;

            for (var index = Number.Position_0; index < groupMembers.Count; index++)
            {
                do
                {
                    randomIndex = random.Next(groupMembers.Count);
                } while (index == randomIndex || frequency[randomIndex] != Number.Number_0);

                if (randomIndex != Number.Number_1_Negative)
                {
                    frequency[randomIndex]++;
                    listPeerEvaluations.Add(new PeerEvaluation
                    {
                        AssignedTaskID = assignedTaskId,
                        ID = Guid.NewGuid(),
                        EvaluatingUserEmail = groupMembers[index].Email,
                        UserID = groupMembers[randomIndex].Email
                    });
                }
                randomIndex = Number.Number_1_Negative;
            }

            return listPeerEvaluations;
        }
    }
}
