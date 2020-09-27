using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Repository.Migrations
{
    public partial class Migr_Duplicate_Record : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Groups_GroupUniqueID",
                table: "AssigmentList");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Users_TeacherUserId",
                table: "AssigmentList");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentMembers_Assigments_AssigmentID",
                table: "AssigmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentMembers_AssigmentList_AssigmentListUniqueID",
                table: "AssigmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigments_Users_TeacherUserId",
                table: "Assigments");

            migrationBuilder.DropIndex(
                name: "IX_Assigments_TeacherUserId",
                table: "Assigments");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID",
                table: "AssigmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_GroupUniqueID",
                table: "AssigmentList");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_TeacherUserId",
                table: "AssigmentList");

            migrationBuilder.DropColumn(
                name: "TeacherUserId",
                table: "Assigments");

            migrationBuilder.DropColumn(
                name: "TeacherUserId",
                table: "AssigmentList");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Assigments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssigmentListUniqueID",
                table: "AssigmentMembers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "AssigmentMembers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssigmentListUniqueID1",
                table: "AssigmentMembers",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "AssigmentList",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID1",
                table: "AssigmentList",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "AssigmentList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assigments_UserID",
                table: "Assigments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID1",
                table: "AssigmentMembers",
                column: "AssigmentListUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_GroupUniqueID1",
                table: "AssigmentList",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_UserID",
                table: "AssigmentList",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Groups_GroupUniqueID1",
                table: "AssigmentList",
                column: "GroupUniqueID1",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Users_UserID",
                table: "AssigmentList",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentMembers_Assigments_AssigmentID",
                table: "AssigmentMembers",
                column: "AssigmentID",
                principalTable: "Assigments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentMembers_AssigmentList_AssigmentListUniqueID1",
                table: "AssigmentMembers",
                column: "AssigmentListUniqueID1",
                principalTable: "AssigmentList",
                principalColumn: "AssigmentListUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigments_Users_UserID",
                table: "Assigments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Groups_GroupUniqueID1",
                table: "AssigmentList");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Users_UserID",
                table: "AssigmentList");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentMembers_Assigments_AssigmentID",
                table: "AssigmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentMembers_AssigmentList_AssigmentListUniqueID1",
                table: "AssigmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigments_Users_UserID",
                table: "Assigments");

            migrationBuilder.DropIndex(
                name: "IX_Assigments_UserID",
                table: "Assigments");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID1",
                table: "AssigmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_GroupUniqueID1",
                table: "AssigmentList");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_UserID",
                table: "AssigmentList");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Assigments");

            migrationBuilder.DropColumn(
                name: "AssigmentListUniqueID1",
                table: "AssigmentMembers");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID1",
                table: "AssigmentList");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "AssigmentList");

            migrationBuilder.AddColumn<int>(
                name: "TeacherUserId",
                table: "Assigments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssigmentListUniqueID",
                table: "AssigmentMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "AssigmentMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "AssigmentList",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "TeacherUserId",
                table: "AssigmentList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assigments_TeacherUserId",
                table: "Assigments",
                column: "TeacherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID",
                table: "AssigmentMembers",
                column: "AssigmentListUniqueID");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_GroupUniqueID",
                table: "AssigmentList",
                column: "GroupUniqueID");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_TeacherUserId",
                table: "AssigmentList",
                column: "TeacherUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Groups_GroupUniqueID",
                table: "AssigmentList",
                column: "GroupUniqueID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Users_TeacherUserId",
                table: "AssigmentList",
                column: "TeacherUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentMembers_Assigments_AssigmentID",
                table: "AssigmentMembers",
                column: "AssigmentID",
                principalTable: "Assigments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentMembers_AssigmentList_AssigmentListUniqueID",
                table: "AssigmentMembers",
                column: "AssigmentListUniqueID",
                principalTable: "AssigmentList",
                principalColumn: "AssigmentListUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigments_Users_TeacherUserId",
                table: "Assigments",
                column: "TeacherUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
