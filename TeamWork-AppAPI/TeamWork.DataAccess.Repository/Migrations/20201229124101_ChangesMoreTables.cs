using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangesMoreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupUniqueID = table.Column<Guid>(nullable: false),
                    GroupName = table.Column<string>(nullable: false),
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
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
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
                        name: "FK_Chats_Groups_GroupUniqueID1",
                        column: x => x.GroupUniqueID1,
                        principalTable: "Groups",
                        principalColumn: "GroupUniqueID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssigmentList",
                columns: table => new
                {
                    AssigmentListUniqueID = table.Column<Guid>(nullable: false),
                    DomainName = table.Column<string>(nullable: true),
                    GroupUniqueID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    GroupUniqueID1 = table.Column<Guid>(nullable: true),
                    TeacherUserEmailId = table.Column<string>(nullable: false)
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
                        name: "FK_AssigmentList_Users_TeacherUserEmailId",
                        column: x => x.TeacherUserEmailId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assigments",
                columns: table => new
                {
                    AssigmentID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: true),
                    ChecklistDeadline = table.Column<DateTime>(nullable: true),
                    MaxGroup = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "AssigmentMembers",
                columns: table => new
                {
                    AssigmentMemberID = table.Column<Guid>(nullable: false),
                    AssigmentID = table.Column<Guid>(nullable: false),
                    AssigmentListUniqueID = table.Column<Guid>(nullable: false),
                    TeacherGrade = table.Column<float>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SolutionLink = table.Column<string>(nullable: true),
                    AssigmentListUniqueID1 = table.Column<Guid>(nullable: true)
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
                        name: "FK_CollegueGrades_Assigments_AssigmentID",
                        column: x => x.AssigmentID,
                        principalTable: "Assigments",
                        principalColumn: "AssigmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegueGrades_Users_UserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_GroupUniqueID1",
                table: "AssigmentList",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_TeacherUserEmailId",
                table: "AssigmentList",
                column: "TeacherUserEmailId");

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
                name: "IX_Items_CheckListID",
                table: "Items",
                column: "CheckListID");

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
                name: "AssigmentMembers");

            migrationBuilder.DropTable(
                name: "CollegueGrades");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AssigmentList");

            migrationBuilder.DropTable(
                name: "Assigments");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
