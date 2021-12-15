using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperienceIT.Web.Migrations
{
    public partial class studentIdRemovedFromProgramOranizerMapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramOrganizerMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(nullable: false),
                    OrganizerId = table.Column<int>(nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganizerMapper_OrganizerId",
                table: "ProgramOrganizerMapper",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganizerMapper_ProgramId",
                table: "ProgramOrganizerMapper",
                column: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramOrganizerMapper");
        }
    }
}
