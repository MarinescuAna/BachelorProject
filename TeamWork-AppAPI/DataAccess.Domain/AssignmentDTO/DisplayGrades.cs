using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.AssignmentDTO
{
    public class DisplayGrades
    {
        public string Fullname { get; set; }
        public string GroupName { get; set; }
        public string AssignmentTitle { get; set; }
        public string GradeChecklist { get; set; }
        public string GradeTeacher { get; set; }
        public string GradePeerEvaluation { get; set; }
        public string Comment { get; set; }
    }
}
