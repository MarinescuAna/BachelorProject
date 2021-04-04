using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangeTypeUserIDAssignmentList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Users_TeacherUserEmailId",
                table: "AssigmentList");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_TeacherUserEmailId",
                table: "AssigmentList");

            migrationBuilder.DropColumn(
                name: "TeacherUserEmailId",
                table: "AssigmentList");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "AssigmentList",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_UserID",
                table: "AssigmentList",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Users_UserID",
                table: "AssigmentList",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserEmailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssigmentList_Users_UserID",
                table: "AssigmentList");

            migrationBuilder.DropIndex(
                name: "IX_AssigmentList_UserID",
                table: "AssigmentList");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "AssigmentList",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherUserEmailId",
                table: "AssigmentList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AssigmentList_TeacherUserEmailId",
                table: "AssigmentList",
                column: "TeacherUserEmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssigmentList_Users_TeacherUserEmailId",
                table: "AssigmentList",
                column: "TeacherUserEmailId",
                principalTable: "Users",
                principalColumn: "UserEmailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
