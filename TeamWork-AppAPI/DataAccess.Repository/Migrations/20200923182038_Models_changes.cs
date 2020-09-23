using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Repository.Migrations
{
    public partial class Models_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Groups_GroupUniqueID1",
                table: "AssigmentList");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Users_UserId",
                table: "AssigmentList");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentMembers_Assigments_AssigmentID",
                table: "AssigmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentMembers_AssigmentList_AssigmentListUniqueID1",
                table: "AssigmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigments_Users_UserId",
                table: "Assigments");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Groups_GroupUniqueID1",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Users_UserId",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CollegueGrades_Assigments_AssigmentID",
                table: "CollegueGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_CollegueGrades_Users_UserId",
                table: "CollegueGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID1",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Users_UserId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupUniqueID1",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_Chats_GroupUniqueID1",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Assigments_UserId",
                table: "Assigments");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID1",
                table: "AssigmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_GroupUniqueID1",
                table: "AssigmentList");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_UserId",
                table: "AssigmentList");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID1",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID1",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Assigments");

            migrationBuilder.DropColumn(
                name: "AssigmentListUniqueID1",
                table: "AssigmentMembers");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID1",
                table: "AssigmentList");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AssigmentList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CollegueGrades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "CollegueGrades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CheckLists",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "Chats",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "TeacherUserId",
                table: "Assigments",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssigmentListUniqueID",
                table: "AssigmentMembers",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "AssigmentMembers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "AssigmentList",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "TeacherUserId",
                table: "AssigmentList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupUniqueID",
                table: "GroupMembers",
                column: "GroupUniqueID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GroupUniqueID",
                table: "Chats",
                column: "GroupUniqueID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Groups_GroupUniqueID",
                table: "Chats",
                column: "GroupUniqueID",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_Users_UserId",
                table: "CheckLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CollegueGrades_Assigments_AssigmentID",
                table: "CollegueGrades",
                column: "AssigmentID",
                principalTable: "Assigments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CollegueGrades_Users_UserId",
                table: "CollegueGrades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Groups_GroupUniqueID",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Users_UserId",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CollegueGrades_Assigments_AssigmentID",
                table: "CollegueGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_CollegueGrades_Users_UserId",
                table: "CollegueGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Users_UserId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupUniqueID",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_Chats_GroupUniqueID",
                table: "Chats");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
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
                name: "GroupUniqueID1",
                table: "GroupMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CollegueGrades",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "CollegueGrades",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CheckLists",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID1",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Assigments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssigmentListUniqueID",
                table: "AssigmentMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "AssigmentMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssigmentListUniqueID1",
                table: "AssigmentMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "AssigmentList",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID1",
                table: "AssigmentList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AssigmentList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupUniqueID1",
                table: "GroupMembers",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GroupUniqueID1",
                table: "Chats",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_Assigments_UserId",
                table: "Assigments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentMembers_AssigmentListUniqueID1",
                table: "AssigmentMembers",
                column: "AssigmentListUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_GroupUniqueID1",
                table: "AssigmentList",
                column: "GroupUniqueID1");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_UserId",
                table: "AssigmentList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Groups_GroupUniqueID1",
                table: "AssigmentList",
                column: "GroupUniqueID1",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Users_UserId",
                table: "AssigmentList",
                column: "UserId",
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
                name: "FK_Assigments_Users_UserId",
                table: "Assigments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Groups_GroupUniqueID1",
                table: "Chats",
                column: "GroupUniqueID1",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_Users_UserId",
                table: "CheckLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollegueGrades_Assigments_AssigmentID",
                table: "CollegueGrades",
                column: "AssigmentID",
                principalTable: "Assigments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollegueGrades_Users_UserId",
                table: "CollegueGrades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupUniqueID1",
                table: "GroupMembers",
                column: "GroupUniqueID1",
                principalTable: "Groups",
                principalColumn: "GroupUniqueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Users_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
