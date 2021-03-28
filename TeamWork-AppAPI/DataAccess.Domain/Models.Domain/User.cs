using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamWork.DataAccess.Domain.Models.Domain
{
    public enum Role{
        STUDENT,
        TEACHER
    };
    public class User
    {
        [Key]
        public string UserEmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Institution { get; set; }
        public Role UserRole { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? AccessTokenExpiration { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<CollegueGrade> CollegueGrades { get; set; }
        public ICollection<CheckList> CheckLists { get; set; }
        public ICollection<AssigmentList> AssigmentLists { get; set; }
        public ICollection<Assigment> Assigments { get; set; }
    }
}
