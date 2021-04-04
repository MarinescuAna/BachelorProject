using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class UpdateAssignmentList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegueGrades_Assigments_AssigmentID",
                table: "CollegueGrades");

            migrationBuilder.DropTable(
                name: "AssigmentMembers");

            migrationBuilder.DropTable(
                name: "Assigments");

            migrationBuilder.DropTable(
                name: "AssigmentList");

            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    ListID = table.Column<Guid>(nullable: false),
                    Domain = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    ListDeadline = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    GroupUniqueID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.ListID);
                    table.ForeignKey(
                        name: "FK_List_Groups_GroupUniqueID",
                        column: x => x.GroupUniqueID,
                        principalTable: "Groups",
                        principalColumn: "GroupUniqueID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_List_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: true),
                    ChecklistDeadline = table.Column<DateTime>(nullable: true),
                    GroupsMax = table.Column<int>(nullable: false),
                    GroupsTake = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    ListID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_Assignments_List_ListID",
                        column: x => x.ListID,
                        principalTable: "List",
                        principalColumn: "ListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedTasks",
                columns: table => new
                {
                    AssignedTaskID = table.Column<Guid>(nullable: false),
                    AssignmentID = table.Column<Guid>(nullable: true),
                    GroupID = table.Column<Guid>(nullable: true),
                    TeacherGrade = table.Column<float>(nullable: false),
                    SolutionLink = table.Column<string>(nullable: true),
                    ListID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTasks", x => x.AssignedTaskID);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_Assignments_AssignmentID",
                        column: x => x.AssignmentID,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupUniqueID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_List_ListID",
                        column: x => x.ListID,
                        principalTable: "List",
                        principalColumn: "ListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_AssignmentID",
                table: "AssignedTasks",
                column: "AssignmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_GroupID",
                table: "AssignedTasks",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_ListID",
                table: "AssignedTasks",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ListID",
                table: "Assignments",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_UserID",
                table: "Assignments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_List_GroupUniqueID",
                table: "List",
                column: "GroupUniqueID");

            migrationBuilder.CreateIndex(
                name: "IX_List_UserID",
                table: "List",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollegueGrades_Assignments_AssigmentID",
                table: "CollegueGrades",
                column: "AssigmentID",
                principalTable: "Assignments",
                principalColumn: "AssignmentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegueGrades_Assignments_AssigmentID",
                table: "CollegueGrades");

            migrationBuilder.DropTable(
                name: "AssignedTasks");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "List");

            migrationBuilder.CreateTable(
                name: "AssigmentList",
                columns: table => new
                {
                    AssigmentListUniqueID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupUniqueID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupUniqueID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigmentList", x => x.AssigmentListUniqueID);
                    table.ForeignKey(
                        name: "FK_AssigmentList_Groups_GroupUniqueID1",
                        column: x => x.GroupUniqueID1,
                        principalTable: "Groups",
                        principalColumn: "GroupUniqueID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssigmentList_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assigments",
                columns: table => new
                {
                    AssigmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChecklistDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxGroup = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigments", x => x.AssigmentID);
                    table.ForeignKey(
                        name: "FK_Assigments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssigmentMembers",
                columns: table => new
                {
                    AssigmentMemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigmentListUniqueID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigmentListUniqueID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SolutionLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeacherGrade = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigmentMembers", x => x.AssigmentMemberID);
                    table.ForeignKey(
                        name: "FK_AssigmentMembers_Assigments_AssigmentID",
                        column: x => x.AssigmentID,
                        principalTable: "Assigments",
                        principalColumn: "AssigmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssigmentMembers_AssigmentList_AssigmentListUniqueID1",
                        column: x => x.AssigmentListUniqueID1,
                        principalTable: "AssigmentList",
                        principalColumn: "AssigmentListUniqueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_GroupUniqueID1",
                table: "AssigmentList",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_UserID",
                table: "AssigmentList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentMembers_AssigmentID",
                table: "AssigmentMembers",
                column: "AssigmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID1",
                table: "AssigmentMembers",
                column: "AssigmentListUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_Assigments_UserID",
                table: "Assigments",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollegueGrades_Assigments_AssigmentID",
                table: "CollegueGrades",
                column: "AssigmentID",
                principalTable: "Assigments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
