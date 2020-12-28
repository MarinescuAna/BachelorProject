using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataAccess.Repository.Migrations
{
    public partial class Mig_GroupMember_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Users_UserID",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupUniqueID",
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "GroupMembers",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Users_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "GroupMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Users_UserID",
                table: "GroupMembers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
