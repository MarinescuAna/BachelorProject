using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangeList3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_List_Users_UserID",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_List_UserID",
                table: "List");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "List",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "List",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_List_UserID",
                table: "List",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_List_Users_UserID",
                table: "List",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserEmailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
