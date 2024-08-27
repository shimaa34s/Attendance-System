using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_System.Migrations
{
    /// <inheritdoc />
    public partial class program : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentHrs");

            migrationBuilder.AddColumn<int>(
                name: "Hnum",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgramCrsId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    CrsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrsName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.CrsId);
                });

            migrationBuilder.CreateTable(
                name: "StudentProgram",
                columns: table => new
                {
                    stId = table.Column<int>(type: "int", nullable: false),
                    pid = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgram", x => new { x.pid, x.stId });
                    table.ForeignKey(
                        name: "FK_StudentProgram_Program_pid",
                        column: x => x.pid,
                        principalTable: "Program",
                        principalColumn: "CrsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProgram_Students_stId",
                        column: x => x.stId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Hnum",
                table: "Students",
                column: "Hnum");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ProgramCrsId",
                table: "Departments",
                column: "ProgramCrsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgram_stId",
                table: "StudentProgram",
                column: "stId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Program_ProgramCrsId",
                table: "Departments",
                column: "ProgramCrsId",
                principalTable: "Program",
                principalColumn: "CrsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Hrs_Hnum",
                table: "Students",
                column: "Hnum",
                principalTable: "Hrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Program_ProgramCrsId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hrs_Hnum",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentProgram");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Students_Hnum",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ProgramCrsId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Hnum",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgramCrsId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "StudentHrs",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false),
                    HId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHrs", x => new { x.SId, x.HId });
                    table.ForeignKey(
                        name: "FK_StudentHrs_Hrs_HId",
                        column: x => x.HId,
                        principalTable: "Hrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentHrs_Students_SId",
                        column: x => x.SId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentHrs_HId",
                table: "StudentHrs",
                column: "HId");
        }
    }
}
