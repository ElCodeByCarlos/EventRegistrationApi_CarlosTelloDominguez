using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistrationApi_CarlosTelloDominguez.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataSecondTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Capacity",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Capacity",
                value: 100);
        }
    }
}
