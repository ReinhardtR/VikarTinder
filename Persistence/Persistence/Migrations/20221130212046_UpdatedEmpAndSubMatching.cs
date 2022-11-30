using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEmpAndSubMatching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerSubstitute_Employers_EmployersId",
                table: "EmployerSubstitute");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerSubstitute_Substitutes_SubstitutesId",
                table: "EmployerSubstitute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployerSubstitute",
                table: "EmployerSubstitute");

            migrationBuilder.DropIndex(
                name: "IX_EmployerSubstitute_SubstitutesId",
                table: "EmployerSubstitute");

            migrationBuilder.RenameColumn(
                name: "SubstitutesId",
                table: "EmployerSubstitute",
                newName: "WantsToMatch");

            migrationBuilder.RenameColumn(
                name: "EmployersId",
                table: "EmployerSubstitute",
                newName: "SubstituteId");

            migrationBuilder.AddColumn<bool>(
                name: "WantsToMatch",
                table: "GigSubstitute",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "EmployerSubstitute",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "EmployerSubstitute",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployerSubstitute",
                table: "EmployerSubstitute",
                columns: new[] { "EmployerId", "SubstituteId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerSubstitute_SubstituteId",
                table: "EmployerSubstitute",
                column: "SubstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerSubstitute_Employers_EmployerId",
                table: "EmployerSubstitute",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerSubstitute_Substitutes_SubstituteId",
                table: "EmployerSubstitute",
                column: "SubstituteId",
                principalTable: "Substitutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerSubstitute_Employers_EmployerId",
                table: "EmployerSubstitute");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerSubstitute_Substitutes_SubstituteId",
                table: "EmployerSubstitute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployerSubstitute",
                table: "EmployerSubstitute");

            migrationBuilder.DropIndex(
                name: "IX_EmployerSubstitute_SubstituteId",
                table: "EmployerSubstitute");

            migrationBuilder.DropColumn(
                name: "WantsToMatch",
                table: "GigSubstitute");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "EmployerSubstitute");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "EmployerSubstitute");

            migrationBuilder.RenameColumn(
                name: "WantsToMatch",
                table: "EmployerSubstitute",
                newName: "SubstitutesId");

            migrationBuilder.RenameColumn(
                name: "SubstituteId",
                table: "EmployerSubstitute",
                newName: "EmployersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployerSubstitute",
                table: "EmployerSubstitute",
                columns: new[] { "EmployersId", "SubstitutesId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerSubstitute_SubstitutesId",
                table: "EmployerSubstitute",
                column: "SubstitutesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerSubstitute_Employers_EmployersId",
                table: "EmployerSubstitute",
                column: "EmployersId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerSubstitute_Substitutes_SubstitutesId",
                table: "EmployerSubstitute",
                column: "SubstitutesId",
                principalTable: "Substitutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
