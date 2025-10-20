using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgAmuasiBooking.Migrations
{
    /// <inheritdoc />
    public partial class Booker1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bookings_aspnetusers_userid",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_userid_title",
                table: "bookings");

            migrationBuilder.AlterColumn<Guid>(
                name: "userid",
                table: "bookings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "bookings",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "aspnetusers",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "ak_aspnetusers_username",
                table: "aspnetusers",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_userid",
                table: "bookings",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_username_title",
                table: "bookings",
                columns: new[] { "username", "title" });

            migrationBuilder.CreateIndex(
                name: "IX_aspnetusers_username",
                table: "aspnetusers",
                column: "username");

            migrationBuilder.AddForeignKey(
                name: "fk_bookings_applicationuser_userid",
                table: "bookings",
                column: "username",
                principalTable: "aspnetusers",
                principalColumn: "username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bookings_applicationuser_userid",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_userid",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_username_title",
                table: "bookings");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_aspnetusers_username",
                table: "aspnetusers");

            migrationBuilder.DropIndex(
                name: "IX_aspnetusers_username",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "username",
                table: "bookings");

            migrationBuilder.AlterColumn<Guid>(
                name: "userid",
                table: "bookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "aspnetusers",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70);

            migrationBuilder.CreateIndex(
                name: "IX_bookings_userid_title",
                table: "bookings",
                columns: new[] { "userid", "title" });

            migrationBuilder.AddForeignKey(
                name: "fk_bookings_aspnetusers_userid",
                table: "bookings",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
