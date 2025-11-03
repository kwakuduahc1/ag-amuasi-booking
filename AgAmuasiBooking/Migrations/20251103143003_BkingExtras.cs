using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgAmuasiBooking.Migrations
{
    /// <inheritdoc />
    public partial class BkingExtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "extraservices",
                columns: table => new
                {
                    extraservicesid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    servicename = table.Column<string>(type: "text", nullable: false),
                    bookingsid = table.Column<Guid>(type: "uuid", nullable: false),
                    persons = table.Column<short>(type: "smallint", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    biller = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    datebilled = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isaccepted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_extraservices", x => x.extraservicesid);
                    table.ForeignKey(
                        name: "fk_extraservices_bookings_bookingsid",
                        column: x => x.bookingsid,
                        principalTable: "bookings",
                        principalColumn: "bookingsid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_extraservices_bookingsid_servicename",
                table: "extraservices",
                columns: new[] { "bookingsid", "servicename" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "extraservices");
        }
    }
}
