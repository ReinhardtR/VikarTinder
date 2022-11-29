using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class DateTrackingEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "Messages",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Messages",
                newName: "SentAt");
        }
    }
}
