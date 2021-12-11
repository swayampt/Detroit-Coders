using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperienceIT.Web.Migrations
{
    public partial class VolunteerEventMapperAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolunteerEventMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    VolunteerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerEventMapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerEventMapper_EventMaster_EventId",
                        column: x => x.EventId,
                        principalTable: "EventMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerEventMapper_VolunteerMaster_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "VolunteerMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEventMapper_EventId",
                table: "VolunteerEventMapper",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEventMapper_VolunteerId",
                table: "VolunteerEventMapper",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerEventMapper");
        }
    }
}
