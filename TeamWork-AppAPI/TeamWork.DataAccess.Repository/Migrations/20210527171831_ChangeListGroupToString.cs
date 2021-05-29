using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangeListGroupToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_List_Groups_GroupID",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_List_GroupID",
                table: "List");

            migrationBuilder.AlterColumn<string>(
                name: "GroupID",
                table: "List",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "GroupID",
                table: "List",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
