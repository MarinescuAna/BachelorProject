using Microsoft.EntityFrameworkCore;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.DataAccess.Repository
{
    public class TeamWorkDbContext:DbContext
    {
        public TeamWorkDbContext(DbContextOptions<TeamWorkDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Assigment> Assigments { get; set; }
        public DbSet<AssigmentList> AssigmentList { get; set; }
        public DbSet<AssigmentMember> AssigmentMembers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<CollegueGrade> CollegueGrades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Message> Messages { get; set; }
        public string JwTokenGenerator { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
