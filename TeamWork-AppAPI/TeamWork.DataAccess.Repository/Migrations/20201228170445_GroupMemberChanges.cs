using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Repository.Migrations
{
    public partial class GroupMemberChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Users_UserId",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupUniqueID",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID",
                table: "GroupMembers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GroupMembers",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "GroupMembers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupID",
                table: "GroupMembers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupID",
                table: "GroupMembers",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupID",
                table: "GroupMembers",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Users_UserID",
                table: "GroupMembers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupID",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Users_UserID",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupID",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "GroupMembers");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "GroupMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_UserID",
                table: "GroupMembers",
                newName: "IX_GroupMembers_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GroupMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID",
                table: "GroupMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupUniqueID",
                table: "GroupMembers",
                column: "GroupUniqueID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID",
                table: "GroupMembers",
                column: "GroupUniqueID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Users_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
