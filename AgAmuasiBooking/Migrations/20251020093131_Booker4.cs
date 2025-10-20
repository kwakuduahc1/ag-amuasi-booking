using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgAmuasiBooking.Migrations
{
    /// <inheritdoc />
    public partial class Booker4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookingservices_bookingsid",
                table: "bookingservices");

            migrationBuilder.DropIndex(
                name: "IX_bookingservices_servicecostsid",
                table: "bookingservices");

            migrationBuilder.AddColumn<DateTime>(
                name: "datecancelled",
                table: "bookings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "iscancelled",
                table: "bookings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isclosed",
                table: "bookings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_bookingservices_bookingsid_servicecostsid",
                table: "bookingservices",
                columns: new[] { "bookingsid", "servicecostsid" });

            migrationBuilder.CreateIndex(
                name: "IX_bookingservices_servicecostsid_bookingsid",
                table: "bookingservices",
                columns: new[] { "servicecostsid", "bookingsid" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookingservices_bookingsid_servicecostsid",
                table: "bookingservices");

            migrationBuilder.DropIndex(
                name: "IX_bookingservices_servicecostsid_bookingsid",
                table: "bookingservices");

            migrationBuilder.DropColumn(
                name: "datecancelled",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "iscancelled",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "isclosed",
                table: "bookings");

            migrationBuilder.CreateIndex(
                name: "IX_bookingservices_bookingsid",
                table: "bookingservices",
                column: "bookingsid");

            migrationBuilder.CreateIndex(
                name: "IX_bookingservices_servicecostsid",
                table: "bookingservices",
                column: "servicecostsid");
        }
    }
}
