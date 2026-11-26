using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventRegistrationApi_CarlosTelloDominguez.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Capacity", "EventDate", "Location", "Title" },
                values: new object[,]
                {
                    { 1, 200, new DateTime(2026, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sheridan College", "Intro to Programming" },
                    { 2, 50, new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sheridan College", "Swimming WorkShop" },
                    { 3, 100, new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "UOFT College", "Calculus WorkShop" },
                    { 4, 150, new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterloo University", "Security Workshop" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "Email", "EventId", "RegisteredOn", "StudentName" },
                values: new object[,]
                {
                    { 1, "carlos@email.com", 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos Tello Dominguez" },
                    { 2, "john@email.com", 2, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe" },
                    { 3, "aaron@email.com", 3, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aaron Smith" },
                    { 4, "alice@email.com", 4, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Tank" },
                    { 5, "bob@email.com", 1, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob Brown" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
