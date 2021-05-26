using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_List_Groups_GroupUniqueID",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_List_GroupUniqueID",
                table: "List");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID",
                table: "List");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ListDeadline",
                table: "List",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupID",
                table: "List",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_List_GroupID",
                table: "List",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_List_Groups_GroupID",
                table: "List",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_List_Groups_GroupID",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_List_GroupID",
                table: "List");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "List");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ListDeadline",
                table: "List",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID",
                table: "List",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_List_GroupUniqueID",
                table: "List",
                column: "GroupUniqueID");

            migrationBuilder.AddForeignKey(
                name: "FK_List_Groups_GroupUniqueID",
                table: "List",
                column: "GroupUniqueID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
