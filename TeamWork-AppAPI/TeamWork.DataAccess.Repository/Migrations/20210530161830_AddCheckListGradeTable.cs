using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class AddCheckListGradeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_ListID",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "ListID",
                table: "AssignedTasks");

            migrationBuilder.CreateTable(
                name: "CheckListGrades",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Grade = table.Column<float>(nullable: false),
                    AssignedTaskID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListGrades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckListGrades_AssignedTasks_AssignedTaskID",
                        column: x => x.AssignedTaskID,
                        principalTable: "AssignedTasks",
                        principalColumn: "AssignedTaskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckListGrades_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListGrades_AssignedTaskID",
                table: "CheckListGrades",
                column: "AssignedTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListGrades_UserID",
                table: "CheckListGrades",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListGrades");

            migrationBuilder.AddColumn<Guid>(
                name: "ListID",
                table: "AssignedTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_ListID",
                table: "AssignedTasks",
                column: "ListID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks",
                column: "ListID",
                principalTable: "List",
                principalColumn: "ListID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
