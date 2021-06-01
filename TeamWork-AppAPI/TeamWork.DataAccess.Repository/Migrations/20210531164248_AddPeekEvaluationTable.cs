using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class AddPeekEvaluationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegueGrades");

            migrationBuilder.CreateTable(
                name: "PeerEvaluations",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EvaluatingUserEmail = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    Grade = table.Column<float>(nullable: false),
                    AssignedTaskID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerEvaluations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PeerEvaluations_AssignedTasks_AssignedTaskID",
                        column: x => x.AssignedTaskID,
                        principalTable: "AssignedTasks",
                        principalColumn: "AssignedTaskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeerEvaluations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluations_AssignedTaskID",
                table: "PeerEvaluations",
                column: "AssignedTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluations_UserID",
                table: "PeerEvaluations",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeerEvaluations");

            migrationBuilder.CreateTable(
                name: "CollegueGrades",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegueGrades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CollegueGrades_Assignments_AssigmentID",
                        column: x => x.AssigmentID,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegueGrades_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollegueGrades_AssigmentID",
                table: "CollegueGrades",
                column: "AssigmentID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegueGrades_UserId",
                table: "CollegueGrades",
                column: "UserId");
        }
    }
}
