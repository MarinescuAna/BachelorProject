using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class CheckListChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    CheckID = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsChecked = table.Column<int>(nullable: false),
                    AssignedTaskID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.CheckID);
                    table.ForeignKey(
                        name: "FK_Checks_AssignedTasks_AssignedTaskID",
                        column: x => x.AssignedTaskID,
                        principalTable: "AssignedTasks",
                        principalColumn: "AssignedTaskID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checks_AssignedTaskID",
                table: "Checks",
                column: "AssignedTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_UserId",
                table: "Checks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.CreateTable(
                name: "CheckLists",
                columns: table => new
                {
                    CheckListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.CheckListID);
                    table.ForeignKey(
                        name: "FK_CheckLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserEmailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_CheckLists_CheckListID",
                        column: x => x.CheckListID,
                        principalTable: "CheckLists",
                        principalColumn: "CheckListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_UserId",
                table: "CheckLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CheckListID",
                table: "Items",
                column: "CheckListID");
        }
    }
}
