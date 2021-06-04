using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamWork.DataAccess.Repository.Migrations
{
    public partial class ChangesAverageGradeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChecklistGradePercent",
                table: "Averages");

            migrationBuilder.DropColumn(
                name: "FinalGrade",
                table: "Averages");

            migrationBuilder.DropColumn(
                name: "ListID",
                table: "Averages");

            migrationBuilder.DropColumn(
                name: "PeerGradePercent",
                table: "Averages");

            migrationBuilder.DropColumn(
                name: "TeacherGradePercent",
                table: "Averages");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedTaskID",
                table: "Averages",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "GradePerAssignedTask",
                table: "Averages",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradePerAssignedTask",
                table: "Averages");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedTaskID",
                table: "Averages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "ChecklistGradePercent",
                table: "Averages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "FinalGrade",
                table: "Averages",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "ListID",
                table: "Averages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeerGradePercent",
                table: "Averages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherGradePercent",
                table: "Averages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
