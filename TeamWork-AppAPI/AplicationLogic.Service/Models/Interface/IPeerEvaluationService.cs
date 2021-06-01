using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IPeerEvaluationService
    {
        Task<PeerEvaluation> GetPeerEvaluationByIdAsync(Guid id);
        Task<bool> UpdatePeerEvaluationAsync(PeerEvaluation peerEvaluation);
        Task<PeerEvaluation> GetPeerEvaluationByAssignedTaskIdAndEmailAsync(Guid assignedTaskId, string currentUserEmail);
        Task<PeerEvaluation> GetCurrentUserGradeByAssignedTaskIdAndEmailAsync(Guid assignedTaskId, string currentUserEmail);
        Task<bool> InsertPeerEvaluationAsync(PeerEvaluation peerEvaluation);
    }
}
