using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationIdslisttocustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationIds",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 22, 11, 44, 30, 565, DateTimeKind.Local).AddTicks(6532));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationIds",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 22, 11, 8, 9, 646, DateTimeKind.Local).AddTicks(6003));
        }
    }
}
