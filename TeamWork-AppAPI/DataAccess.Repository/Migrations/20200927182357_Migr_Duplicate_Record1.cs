using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Repository.Migrations
{
    public partial class Migr_Duplicate_Record1 : Migration
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

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "GroupMembers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID1",
                table: "GroupMembers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupUniqueID1",
                table: "GroupMembers",
                column: "GroupUniqueID1");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID1",
                table: "GroupMembers",
                column: "GroupUniqueID1",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_GroupMembers_Groups_GroupUniqueID1",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Users_UserID",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupUniqueID1",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID1",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "GroupMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

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
