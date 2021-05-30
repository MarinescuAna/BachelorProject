using Microsoft.EntityFrameworkCore;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.DataAccess.Repository
{
    public class TeamWorkDbContext:DbContext
    {
        public TeamWorkDbContext(DbContextOptions<TeamWorkDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<List> List { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<CheckListGrade> CheckListGrades { get; set; }
        public DbSet<CollegueGrade> CollegueGrades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
