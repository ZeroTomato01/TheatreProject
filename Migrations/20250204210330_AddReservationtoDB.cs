using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationId", "AmountOfTickets", "CustomerId", "TheatreShowDateId", "Used" },
                values: new object[] { 1, 1, null, 1, false });

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 11, 22, 3, 29, 679, DateTimeKind.Local).AddTicks(6490));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 2,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 28, 22, 3, 29, 679, DateTimeKind.Local).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 3,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 7, 22, 3, 29, 679, DateTimeKind.Local).AddTicks(6585));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "ReservationId",
                keyValue: 1);

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
        }
    }
}
