using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTheatreShowFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow");

            migrationBuilder.AlterColumn<int>(
                name: "VenueId",
                table: "TheatreShow",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "TheatreShow",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow",
                column: "VenueId",
                principalTable: "Venue",
                principalColumn: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow");

            migrationBuilder.AlterColumn<int>(
                name: "VenueId",
                table: "TheatreShow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "TheatreShow",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 7, 12, 19, 25, 158, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 2,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 24, 12, 19, 25, 158, DateTimeKind.Local).AddTicks(6931));

            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 3,
                column: "DateAndTime",
                value: new DateTime(2025, 2, 3, 12, 19, 25, 158, DateTimeKind.Local).AddTicks(6951));

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow",
                column: "VenueId",
                principalTable: "Venue",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
