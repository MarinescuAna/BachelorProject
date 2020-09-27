using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Repository.Migrations
{
    public partial class Migr_Duplicates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Chats_GroupUniqueID",
                table: "Chats");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CollegueGrades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "CollegueGrades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CheckLists",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "Chats",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUniqueID1",
                table: "Chats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GroupUniqueID1",
                table: "Chats",
                column: "GroupUniqueID1");

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Chats_GroupUniqueID1",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "GroupUniqueID1",
                table: "Chats");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CollegueGrades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AssigmentID",
                table: "CollegueGrades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CheckLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupUniqueID",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GroupUniqueID",
                table: "Chats",
                column: "GroupUniqueID");

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
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
