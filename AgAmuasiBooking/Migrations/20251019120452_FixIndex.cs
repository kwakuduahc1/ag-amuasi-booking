using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgAmuasiBooking.Migrations
{
    /// <inheritdoc />
    public partial class FixIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_services_servicename",
                table: "services");

            migrationBuilder.AddColumn<bool>(
                name: "perperson",
                table: "services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "servicesid",
                keyValue: 1,
                column: "perperson",
                value: false);

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "servicesid",
                keyValue: 2,
                column: "perperson",
                value: false);

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "servicesid",
                keyValue: 3,
                column: "perperson",
                value: false);

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "servicesid",
                keyValue: 4,
                column: "perperson",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "perperson",
                table: "services");

            migrationBuilder.CreateIndex(
                name: "IX_services_servicename",
                table: "services",
                column: "servicename",
                unique: true);
        }
    }
}
