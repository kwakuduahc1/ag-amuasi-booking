using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgAmuasiBooking.Migrations
{
    /// <inheritdoc />
    public partial class Booker2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bookings_applicationuser_userid",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_userid",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userid",
                table: "bookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookings_userid",
                table: "bookings",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "fk_bookings_applicationuser_userid",
                table: "bookings",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id");
        }
    }
}
