using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelHai.CS.ServerAPI.Migrations
{
    /// <inheritdoc />
    public partial class addgradeerror : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _grade = table.Column<double>(type: "float", nullable: false),
                    ExamId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Exams_ExamId1",
                        column: x => x.ExamId1,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChosenAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Errors_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Errors_GradeId",
                table: "Errors",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ExamId1",
                table: "Grades",
                column: "ExamId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
