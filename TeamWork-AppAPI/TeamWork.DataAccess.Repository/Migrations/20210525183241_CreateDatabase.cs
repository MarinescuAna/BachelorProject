using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupUniqueID = table.Column<Guid>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupUniqueID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserEmailId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    AccessToken = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    AccessTokenExpiration = table.Column<DateTime>(nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserEmailId);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatID = table.Column<Guid>(nullable: false),
                    GroupUniqueID = table.Column<Guid>(nullable: false),
                    GroupUniqueID1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatID);
                    table.ForeignKey(
                        name: "FK_Chats_Groups_GroupUniqueID",
                        column: x => x.GroupUniqueID1,
                        principalTable: "Groups",
                        principalColumn: "GroupUniqueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckLists",
                columns: table => new
                {
                    CheckListID = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.CheckListID);
                    table.ForeignKey(
                        name: "FK_CheckLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupMemberID = table.Column<Guid>(nullable: false),
                    StatusRequest = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    GroupID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.GroupMemberID);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupUniqueID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(nullable: false),
                    ImageContent = table.Column<string>(nullable: true),
                    ImageExtention = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    ListID = table.Column<Guid>(nullable: false),
                    Domain = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    ListDeadline = table.Column<DateTime>(nullable: false),
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
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ChatID = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "Chats",
                        principalColumn: "ChatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CheckListID = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_CheckLists_CheckListID",
                        column: x => x.CheckListID,
                        principalTable: "CheckLists",
                        principalColumn: "CheckListID",
                        onDelete: ReferentialAction.Cascade);
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
                    ListID = table.Column<Guid>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AssignedTasks",
                columns: table => new
                {
                    AssignedTaskID = table.Column<Guid>(nullable: false),
                    AssignmentID = table.Column<Guid>(nullable: false),
                    GroupID = table.Column<Guid>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "CollegueGrades",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Grade = table.Column<float>(nullable: false),
                    AssigmentID = table.Column<Guid>(nullable: false)
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
                name: "IX_Chats_GroupUniqueID1",
                table: "Chats",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_UserId",
                table: "CheckLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollegueGrades_AssigmentID",
                table: "CollegueGrades",
                column: "AssigmentID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegueGrades_UserId",
                table: "CollegueGrades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupID",
                table: "GroupMembers",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserID",
                table: "GroupMembers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CheckListID",
                table: "Items",
                column: "CheckListID");

            migrationBuilder.CreateIndex(
                name: "IX_List_GroupUniqueID",
                table: "List",
                column: "GroupUniqueID");

            migrationBuilder.CreateIndex(
                name: "IX_List_UserID",
                table: "List",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatID",
                table: "Messages",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedTasks");

            migrationBuilder.DropTable(
                name: "CollegueGrades");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "List");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
