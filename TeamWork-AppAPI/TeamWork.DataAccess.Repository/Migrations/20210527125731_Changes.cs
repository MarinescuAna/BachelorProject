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

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_GroupID",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "AssignedTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ListID",
                table: "AssignedTasks",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks",
                column: "ListID",
                principalTable: "List",
                principalColumn: "ListID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ListID",
                table: "AssignedTasks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

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

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_List_ListID",
                table: "AssignedTasks",
                column: "ListID",
                principalTable: "List",
                principalColumn: "ListID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
