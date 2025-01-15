using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreProject.Migrations
{
    /// <inheritdoc />
    public partial class fixcirculardepencyTheatreshow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 22, 11, 8, 9, 646, DateTimeKind.Local).AddTicks(6003));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TheatreShowDate",
                keyColumn: "TheatreShowDateId",
                keyValue: 1,
                column: "DateAndTime",
                value: new DateTime(2025, 1, 22, 10, 32, 35, 152, DateTimeKind.Local).AddTicks(3619));
        }
    }
}
