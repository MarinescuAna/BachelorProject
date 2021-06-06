using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class PeerEvaluationServiceImpl:ABaseService,IPeerEvaluationService
    {
        public PeerEvaluationServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }

        public async Task<PeerEvaluation> GetPeerEvaluationByAssignedTaskIdAndEmailAsync(
            Guid assignedTaskId, 
            string currentUserEmail
            ) => 
            await _unitOfWork.PeerEvaluations.GetItem(u => 
                u.AssignedTaskID == assignedTaskId
                && u.EvaluatingUserEmail == currentUserEmail
            );
        public async Task<PeerEvaluation> GetCurrentUserGradeByAssignedTaskIdAndEmailAsync(
            Guid assignedTaskId, 
            string currentUserEmail
            ) => 
            await _unitOfWork.PeerEvaluations.GetItem(u => 
                u.AssignedTaskID == assignedTaskId
                && u.UserID == currentUserEmail
            );
        public async Task<PeerEvaluation> GetPeerEvaluationByIdAsync(Guid id) =>
            await _unitOfWork.PeerEvaluations.GetItem(u => u.ID == id);
        public async Task<bool> UpdatePeerEvaluationAsync(PeerEvaluation peerEvaluation)
        {
           await _unitOfWork.PeerEvaluations.UpdateItem(peerEvaluation);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }
        public async Task<bool> InsertPeerEvaluationAsync(PeerEvaluation peerEvaluation)
        {
            _unitOfWork.PeerEvaluations.InsertItem(peerEvaluation);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }
    }
}
