using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    StartingDate = table.Column<string>(nullable: true),
                    EndingDate = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "phase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    ParentPhaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phase_phase_ParentPhaseId",
                        column: x => x.ParentPhaseId,
                        principalTable: "phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_phase_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_phase_ParentPhaseId",
                table: "phase",
                column: "ParentPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_phase_ProjectId",
                table: "phase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_EmployeeId",
                table: "project",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "phase");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
