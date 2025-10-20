using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgAmuasiBooking.Migrations
{
    /// <inheritdoc />
    public partial class Booker3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "approver",
                table: "bookings",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "days",
                table: "bookings",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1);

            migrationBuilder.AddColumn<string>(
                name: "receiver",
                table: "bookings",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approver",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "days",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "receiver",
                table: "bookings");
        }
    }
}
