using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewData : Migration
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Venue",
                columns: new[] { "VenueId", "Capacity", "Name" },
                values: new object[] { 1, 300, "The Pen" });

            migrationBuilder.InsertData(
                table: "TheatreShow",
                columns: new[] { "TheatreShowId", "Description", "Price", "Title", "VenueId" },
                values: new object[] { 1, "moo", 20.0, "Cow's Movie", 1 });

            migrationBuilder.InsertData(
                table: "TheatreShowDate",
                columns: new[] { "TheatreShowDateId", "DateAndTime", "TheatreShowId" },
                values: new object[] { 1, new DateTime(2025, 1, 22, 10, 32, 35, 152, DateTimeKind.Local).AddTicks(3619), 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow",
                column: "VenueId",
                principalTable: "Venue",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow");

            migrationBuilder.DeleteData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TheatreShow",
                keyColumn: "TheatreShowId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venue",
                keyColumn: "VenueId",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "VenueId",
                table: "TheatreShow",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreShow_Venue_VenueId",
                table: "TheatreShow",
                column: "VenueId",
                principalTable: "Venue",
                principalColumn: "VenueId");
        }
    }
}
