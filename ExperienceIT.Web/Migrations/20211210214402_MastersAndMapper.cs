using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperienceIT.Web.Migrations
{
    public partial class MastersAndMapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    StartingDate = table.Column<DateTime>(nullable: false),
                    EndingDate = table.Column<DateTime>(nullable: false),
                    EnrollmentDate = table.Column<DateTime>(nullable: false),
                    Venue = table.Column<string>(nullable: false),
                    Duration = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizerMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    State = table.Column<string>(maxLength: 30, nullable: true),
                    Zipcode = table.Column<string>(maxLength: 15, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizerMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    LinkedIn = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    State = table.Column<string>(maxLength: 30, nullable: true),
                    Zipcode = table.Column<string>(maxLength: 15, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    AgeStatus = table.Column<int>(nullable: false),
                    Aboutme = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMaster_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    LinkedIn = table.Column<string>(maxLength: 100, nullable: true),
                    Skills = table.Column<string>(maxLength: 100, nullable: false),
                    YearsOfExperience = table.Column<int>(nullable: false),
                    CurrentOrganization = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    State = table.Column<string>(maxLength: 30, nullable: true),
                    Zipcode = table.Column<string>(maxLength: 15, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    AgeStatus = table.Column<int>(nullable: false),
                    Aboutme = table.Column<string>(nullable: true),
                    Availability = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerMaster_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramEventMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEventMapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramEventMapper_EventMaster_EventId",
                        column: x => x.EventId,
                        principalTable: "EventMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEventMapper_ProgramMaster_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramEventStudentMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEventStudentMapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramEventStudentMapper_EventMaster_EventId",
                        column: x => x.EventId,
                        principalTable: "EventMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEventStudentMapper_ProgramMaster_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEventStudentMapper_StudentMaster_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOrganaizerMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(nullable: false),
                    OrganaizerId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOrganaizerMapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramOrganaizerMapper_OrganizerMaster_OrganaizerId",
                        column: x => x.OrganaizerId,
                        principalTable: "OrganizerMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOrganaizerMapper_ProgramMaster_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOrganaizerMapper_StudentMaster_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramEventVolunteerMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    VolunteerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEventVolunteerMapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramEventVolunteerMapper_EventMaster_EventId",
                        column: x => x.EventId,
                        principalTable: "EventMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEventVolunteerMapper_ProgramMaster_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEventVolunteerMapper_VolunteerMaster_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "VolunteerMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventMapper_EventId",
                table: "ProgramEventMapper",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventMapper_ProgramId",
                table: "ProgramEventMapper",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventStudentMapper_EventId",
                table: "ProgramEventStudentMapper",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventStudentMapper_ProgramId",
                table: "ProgramEventStudentMapper",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventStudentMapper_StudentId",
                table: "ProgramEventStudentMapper",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventVolunteerMapper_EventId",
                table: "ProgramEventVolunteerMapper",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventVolunteerMapper_ProgramId",
                table: "ProgramEventVolunteerMapper",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEventVolunteerMapper_VolunteerId",
                table: "ProgramEventVolunteerMapper",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganaizerMapper_OrganaizerId",
                table: "ProgramOrganaizerMapper",
                column: "OrganaizerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganaizerMapper_ProgramId",
                table: "ProgramOrganaizerMapper",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOrganaizerMapper_StudentId",
                table: "ProgramOrganaizerMapper",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMaster_UserId",
                table: "StudentMaster",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerMaster_UserId",
                table: "VolunteerMaster",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramEventMapper");

            migrationBuilder.DropTable(
                name: "ProgramEventStudentMapper");

            migrationBuilder.DropTable(
                name: "ProgramEventVolunteerMapper");

            migrationBuilder.DropTable(
                name: "ProgramOrganaizerMapper");

            migrationBuilder.DropTable(
                name: "EventMaster");

            migrationBuilder.DropTable(
                name: "VolunteerMaster");

            migrationBuilder.DropTable(
                name: "OrganizerMaster");

            migrationBuilder.DropTable(
                name: "ProgramMaster");

            migrationBuilder.DropTable(
                name: "StudentMaster");
        }
    }
}
