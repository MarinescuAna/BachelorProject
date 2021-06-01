using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class AddCommentsInTableNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "PeerEvaluations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "PeerEvaluations");
        }
    }
}
