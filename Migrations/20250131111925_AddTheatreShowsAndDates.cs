using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class AddTheatreShowsAndDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TheatreShow",
                columns: new[] { "TheatreShowId", "Description", "Price", "TheatreShowDateIds", "Title", "VenueId" },
                values: new object[] { 2, "moo too", 20.0, "[2,3]", "Cow's Movie 2", 1 });

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 7, 12, 19, 25, 158, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.InsertData(
                table: "TheatreShowDate",
                columns: new[] { "TheatreShowDateId", "DateAndTime", "ReservationIds", "TheatreShowId" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 1, 24, 12, 19, 25, 158, DateTimeKind.Local).AddTicks(6931), null, 2 },
                    { 3, new DateTime(2025, 2, 3, 12, 19, 25, 158, DateTimeKind.Local).AddTicks(6951), null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TheatreShow",
                keyColumn: "TheatreShowId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 7, 11, 20, 36, 256, DateTimeKind.Local).AddTicks(8563));
        }
    }
}
