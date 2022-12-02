using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class changeJobConfirmationDatatypeonChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobConfirmations_ChatId",
                table: "JobConfirmations");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmations_ChatId",
                table: "JobConfirmations",
                column: "ChatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobConfirmations_ChatId",
                table: "JobConfirmations");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmations_ChatId",
                table: "JobConfirmations",
                column: "ChatId");
        }
    }
}
