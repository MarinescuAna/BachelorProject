using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class AddNotificationAverageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Averages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistGradePercent = table.Column<int>(nullable: false),
                    TeacherGradePercent = table.Column<int>(nullable: false),
                    PeerGradePercent = table.Column<int>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    StudentID = table.Column<string>(nullable: true),
                    AssignedTaskID = table.Column<Guid>(nullable: false),
                    ListID = table.Column<Guid>(nullable: false),
                    FinalGrade = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Averages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Averages");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
