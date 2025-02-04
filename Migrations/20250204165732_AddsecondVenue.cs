using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class AddsecondVenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 11, 17, 57, 31, 631, DateTimeKind.Local).AddTicks(7103));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 2,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 28, 17, 57, 31, 631, DateTimeKind.Local).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 3,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 7, 17, 57, 31, 631, DateTimeKind.Local).AddTicks(7197));

            migrationBuilder.InsertData(
                table: "Venue",
                columns: new[] { "VenueId", "Capacity", "Name", "TheatreShowIds" },
                values: new object[] { 2, 50, "The Pigsty", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Venue",
                keyColumn: "VenueId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 11, 14, 24, 21, 928, DateTimeKind.Local).AddTicks(1202));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 2,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 28, 14, 24, 21, 928, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 3,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 7, 14, 24, 21, 928, DateTimeKind.Local).AddTicks(1330));
        }
    }
}
