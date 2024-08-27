using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_System.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Program_ProgramCrsId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "StudentProgram");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ProgramCrsId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ProgramCrsId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CrsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrsName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CrsId);
                });

            migrationBuilder.CreateTable(
                name: "CourseDepartment",
                columns: table => new
                {
                    CourseCrsId = table.Column<int>(type: "int", nullable: false),
                    DepartmentDeptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDepartment", x => new { x.CourseCrsId, x.DepartmentDeptId });
                    table.ForeignKey(
                        name: "FK_CourseDepartment_Courses_CourseCrsId",
                        column: x => x.CourseCrsId,
                        principalTable: "Courses",
                        principalColumn: "CrsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseDepartment_Departments_DepartmentDeptId",
                        column: x => x.DepartmentDeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    stId = table.Column<int>(type: "int", nullable: false),
                    crsid = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.crsid, x.stId });
                    table.ForeignKey(
                        name: "FK_StudentCourse_Courses_crsid",
                        column: x => x.crsid,
                        principalTable: "Courses",
                        principalColumn: "CrsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Students_stId",
                        column: x => x.stId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDepartment_DepartmentDeptId",
                table: "CourseDepartment",
                column: "DepartmentDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_stId",
                table: "StudentCourse",
                column: "stId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDepartment");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "Courses");

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
                    pid = table.Column<int>(type: "int", nullable: false),
                    stId = table.Column<int>(type: "int", nullable: false),
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
        }
    }
}
