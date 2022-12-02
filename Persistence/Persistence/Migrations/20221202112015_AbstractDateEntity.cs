using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AbstractDateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobConfirmations_Chats_chatId",
                table: "JobConfirmations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobConfirmations_Users_employerId",
                table: "JobConfirmations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobConfirmations_Users_substituteId",
                table: "JobConfirmations");

            migrationBuilder.RenameColumn(
                name: "substituteId",
                table: "JobConfirmations",
                newName: "SubstituteId");

            migrationBuilder.RenameColumn(
                name: "isAccepted",
                table: "JobConfirmations",
                newName: "IsAccepted");

            migrationBuilder.RenameColumn(
                name: "employerId",
                table: "JobConfirmations",
                newName: "EmployerId");

            migrationBuilder.RenameColumn(
                name: "chatId",
                table: "JobConfirmations",
                newName: "ChatId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "JobConfirmations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_JobConfirmations_substituteId",
                table: "JobConfirmations",
                newName: "IX_JobConfirmations_SubstituteId");

            migrationBuilder.RenameIndex(
                name: "IX_JobConfirmations_employerId",
                table: "JobConfirmations",
                newName: "IX_JobConfirmations_EmployerId");

            migrationBuilder.RenameIndex(
                name: "IX_JobConfirmations_chatId",
                table: "JobConfirmations",
                newName: "IX_JobConfirmations_ChatId");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "JobConfirmations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_JobConfirmations_Chats_ChatId",
                table: "JobConfirmations",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobConfirmations_Users_EmployerId",
                table: "JobConfirmations",
                column: "EmployerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobConfirmations_Users_SubstituteId",
                table: "JobConfirmations",
                column: "SubstituteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobConfirmations_Chats_ChatId",
                table: "JobConfirmations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobConfirmations_Users_EmployerId",
                table: "JobConfirmations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobConfirmations_Users_SubstituteId",
                table: "JobConfirmations");

            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "JobConfirmations");

            migrationBuilder.RenameColumn(
                name: "SubstituteId",
                table: "JobConfirmations",
                newName: "substituteId");

            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "JobConfirmations",
                newName: "isAccepted");

            migrationBuilder.RenameColumn(
                name: "EmployerId",
                table: "JobConfirmations",
                newName: "employerId");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "JobConfirmations",
                newName: "chatId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JobConfirmations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_JobConfirmations_SubstituteId",
                table: "JobConfirmations",
                newName: "IX_JobConfirmations_substituteId");

            migrationBuilder.RenameIndex(
                name: "IX_JobConfirmations_EmployerId",
                table: "JobConfirmations",
                newName: "IX_JobConfirmations_employerId");

            migrationBuilder.RenameIndex(
                name: "IX_JobConfirmations_ChatId",
                table: "JobConfirmations",
                newName: "IX_JobConfirmations_chatId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobConfirmations_Chats_chatId",
                table: "JobConfirmations",
                column: "chatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobConfirmations_Users_employerId",
                table: "JobConfirmations",
                column: "employerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobConfirmations_Users_substituteId",
                table: "JobConfirmations",
                column: "substituteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
