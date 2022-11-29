using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Substitutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gigs_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerSubstitute",
                columns: table => new
                {
                    EmployersId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubstitutesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerSubstitute", x => new { x.EmployersId, x.SubstitutesId });
                    table.ForeignKey(
                        name: "FK_EmployerSubstitute_Employers_EmployersId",
                        column: x => x.EmployersId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerSubstitute_Substitutes_SubstitutesId",
                        column: x => x.SubstitutesId,
                        principalTable: "Substitutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GigSubstitute",
                columns: table => new
                {
                    PositionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubstitutesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigSubstitute", x => new { x.PositionsId, x.SubstitutesId });
                    table.ForeignKey(
                        name: "FK_GigSubstitute_Gigs_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "Gigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GigSubstitute_Substitutes_SubstitutesId",
                        column: x => x.SubstitutesId,
                        principalTable: "Substitutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerSubstitute_SubstitutesId",
                table: "EmployerSubstitute",
                column: "SubstitutesId");

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_EmployerId",
                table: "Gigs",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_GigSubstitute_SubstitutesId",
                table: "GigSubstitute",
                column: "SubstitutesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerSubstitute");

            migrationBuilder.DropTable(
                name: "GigSubstitute");

            migrationBuilder.DropTable(
                name: "Gigs");

            migrationBuilder.DropTable(
                name: "Substitutes");

            migrationBuilder.DropTable(
                name: "Employers");
        }
    }
}
