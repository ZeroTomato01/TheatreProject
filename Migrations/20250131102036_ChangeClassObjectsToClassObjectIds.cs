using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeClassObjectsToClassObjectIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_TheatreShowDate_TheatreShowDateId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TheatreShowDateId",
                table: "Reservation");

            migrationBuilder.AddColumn<string>(
                name: "TheatreShowIds",
                table: "Venue",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservationIds",
                table: "TheatreShowDate",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TheatreShowDateIds",
                table: "TheatreShow",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TheatreShow",
                keyColumn: "TheatreShowId",
                keyValue: 1,
                column: "TheatreShowDateIds",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                columns: new[] { "DateAndTime", "ReservationIds" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 20, 36, 256, DateTimeKind.Local).AddTicks(8563), null });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueId",
                keyValue: 1,
                column: "TheatreShowIds",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TheatreShowIds",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "ReservationIds",
                table: "TheatreShowDate");

            migrationBuilder.DropColumn(
                name: "TheatreShowDateIds",
                table: "TheatreShow");

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 22, 11, 44, 30, 565, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TheatreShowDateId",
                table: "Reservation",
                column: "TheatreShowDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_TheatreShowDate_TheatreShowDateId",
                table: "Reservation",
                column: "TheatreShowDateId",
                principalTable: "TheatreShowDate",
                principalColumn: "TheatreShowDateId");
        }
    }
}
