using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupID",
                table: "List",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ListID",
                table: "AssignedTasks",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_List_GroupID",
                table: "List",
                column: "GroupID");

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_List_Groups_GroupID",
                table: "List",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_List_Groups_GroupID",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_List_GroupID",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_ListID",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "ListID",
                table: "AssignedTasks");

            migrationBuilder.AlterColumn<string>(
                name: "GroupID",
                table: "List",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupID",
                table: "AssignedTasks",
                type: "uniqueidentifier",
                nullable: false,
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
    }
}
