using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_System.Migrations
{
    /// <inheritdoc />
    public partial class att : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "StudentAttendeces");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "StudentAttendeces");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "StudentAttendeces");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "StudentAttendeces");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "StudentAttendeces");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "StudentAttendeces");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "StudentAttendeces");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceDay",
                table: "StudentAttendeces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceDay",
                table: "StudentAttendeces");

            migrationBuilder.AddColumn<bool>(
                name: "Friday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Monday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Saturday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sunday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Thursday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tuesday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wednesday",
                table: "StudentAttendeces",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
