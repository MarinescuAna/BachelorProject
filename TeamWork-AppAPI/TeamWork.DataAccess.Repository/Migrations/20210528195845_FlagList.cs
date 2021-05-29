using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class FlagList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "List");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "List",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "List");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "List",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
