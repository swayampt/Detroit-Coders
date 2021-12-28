using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperienceIT.Web.Migrations
{
    public partial class EventItemImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "EventMaster",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "EventMaster");
        }
    }
}
