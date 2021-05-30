using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangesAtAssignedTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupID",
                table: "AssignedTasks",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_GroupID",
                table: "AssignedTasks",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_Groups_GroupID",
                table: "AssignedTasks",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_Groups_GroupID",
                table: "AssignedTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_GroupID",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "AssignedTasks");
        }
    }
}
