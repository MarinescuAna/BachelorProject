using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.DashboardDTO
{
    public class DashboardData
    {
        public string EmailStudent { get; set; }
        public string Fullname { get; set; }
        public string TasksDone { get; set; }
        public string Tasks { get; set; }
        public string PeerEvaluationGrade { get; set; }
        public string ChecklistEvaluationGrade { get; set; }
        public string Error { get; set; }
    }
}
