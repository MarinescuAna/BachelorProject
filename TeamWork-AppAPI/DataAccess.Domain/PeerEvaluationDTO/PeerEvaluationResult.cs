using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.PeerEvaluationDTO
{
    public class PeerEvaluationResult
    {
        public string Id { get; set; }
        public string EvaluatingStudentEmail { get; set; }
        public string EvaluatingStudentFullname { get; set; }
        public string Error { get; set; }
    }
}
