using HelpDeskMini.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskMini.Data.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261122000100_Initial")]
public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Departments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Departments", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Technicians",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Technicians", x => x.Id));

        migrationBuilder.CreateTable(
            name: "HelpRequests",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Cabinet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                Priority = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                DepartmentId = table.Column<int>(type: "int", nullable: true),
                TechnicianId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HelpRequests", x => x.Id);
                table.ForeignKey("FK_HelpRequests_Departments_DepartmentId", x => x.DepartmentId, "Departments", "Id", onDelete: ReferentialAction.SetNull);
                table.ForeignKey("FK_HelpRequests_Technicians_TechnicianId", x => x.TechnicianId, "Technicians", "Id", onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateIndex(name: "IX_Departments_Name", table: "Departments", column: "Name", unique: true);
        migrationBuilder.CreateIndex(name: "IX_HelpRequests_DepartmentId", table: "HelpRequests", column: "DepartmentId");
        migrationBuilder.CreateIndex(name: "IX_HelpRequests_TechnicianId", table: "HelpRequests", column: "TechnicianId");

        migrationBuilder.InsertData("Departments", new[] { "Id", "Description", "Name" }, new object[,]
        {
            { 1, "Техническа поддръжка на компютри и мрежа", "Информационни технологии" },
            { 2, "Поддръжка на оборудването в кабинетите", "Компютърни кабинети" },
            { 3, "Проектори, екрани и аудио техника", "Мултимедия" }
        });
        migrationBuilder.InsertData("Technicians", new[] { "Id", "Email", "FullName" }, new object[,]
        {
            { 1, "ivan.petrov@example.com", "Иван Петров" },
            { 2, "maria.ivanova@example.com", "Мария Иванова" },
            { 3, "georgi.dimitrov@example.com", "Георги Димитров" }
        });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "HelpRequests");
        migrationBuilder.DropTable(name: "Departments");
        migrationBuilder.DropTable(name: "Technicians");
    }
}
