using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Added_JobConfirmations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobConfirmations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    chatId = table.Column<int>(type: "INTEGER", nullable: false),
                    substituteId = table.Column<int>(type: "INTEGER", nullable: false),
                    employerId = table.Column<int>(type: "INTEGER", nullable: false),
                    isAccepted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobConfirmations", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobConfirmations_Chats_chatId",
                        column: x => x.chatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobConfirmations_Users_employerId",
                        column: x => x.employerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobConfirmations_Users_substituteId",
                        column: x => x.substituteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmations_chatId",
                table: "JobConfirmations",
                column: "chatId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmations_employerId",
                table: "JobConfirmations",
                column: "employerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmations_substituteId",
                table: "JobConfirmations",
                column: "substituteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobConfirmations");
        }
    }
}
