using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interest",
                columns: table => new
                {
                    id_interest = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    interest_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interest", x => x.id_interest);
                });

            migrationBuilder.CreateTable(
                name: "transport_mode",
                columns: table => new
                {
                    id_transport_mode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transport_mode_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_mode", x => x.id_transport_mode);
                });

            migrationBuilder.CreateTable(
                name: "user_interest",
                columns: table => new
                {
                    id_interest = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_interest", x => new { x.id_interest, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_interest_interest_id_interest",
                        column: x => x.id_interest,
                        principalTable: "interest",
                        principalColumn: "id_interest",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_interest_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_transport_mode",
                columns: table => new
                {
                    id_transport_mode = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_transport_mode", x => new { x.id_transport_mode, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_transport_mode_transport_mode_id_transport_mode",
                        column: x => x.id_transport_mode,
                        principalTable: "transport_mode",
                        principalColumn: "id_transport_mode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_transport_mode_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_interest_id_user",
                table: "user_interest",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_transport_mode_id_user",
                table: "user_transport_mode",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_interest");

            migrationBuilder.DropTable(
                name: "user_transport_mode");

            migrationBuilder.DropTable(
                name: "interest");

            migrationBuilder.DropTable(
                name: "transport_mode");
        }
    }
}
