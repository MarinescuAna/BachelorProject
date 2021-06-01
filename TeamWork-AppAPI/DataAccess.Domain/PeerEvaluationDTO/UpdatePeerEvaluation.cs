using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.PeerEvaluationDTO
{
    public class UpdatePeerEvaluation
    {
        public string PeerEvaluationId { get; set; }
        public string Grade { get; set; }
        public string Comments { get; set; }
    }
}
