using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperienceIT.Web.Migrations
{
    public partial class OrganizerAndProgramOrganizerMapperAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "OrganizerMaster",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                   City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                   Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                   Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                   State = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                   Zipcode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_OrganizerMaster", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "ProgramOrganizerMapper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOrganizerMapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramOrganizerMapper_OrganizerMaster_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "OrganizerMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOrganizerMapper_ProgramMaster_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOrganizerMapper_StudentMaster_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganizerMapper_OrganizerId",
                table: "ProgramOrganizerMapper",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganizerMapper_ProgramId",
                table: "ProgramOrganizerMapper",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganizerMapper_StudentId",
                table: "ProgramOrganizerMapper",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }

   
}
