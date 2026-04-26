using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class BlockedUserChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_blocked_users",
                table: "blocked_users");

            migrationBuilder.AddColumn<int>(
                name: "id_blocked",
                table: "blocked_users",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_blocked_users",
                table: "blocked_users",
                column: "id_blocked");

            migrationBuilder.CreateIndex(
                name: "IX_blocked_users_id_user_blocker",
                table: "blocked_users",
                column: "id_user_blocker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_blocked_users",
                table: "blocked_users");

            migrationBuilder.DropIndex(
                name: "IX_blocked_users_id_user_blocker",
                table: "blocked_users");

            migrationBuilder.DropColumn(
                name: "id_blocked",
                table: "blocked_users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blocked_users",
                table: "blocked_users",
                columns: new[] { "id_user_blocker", "id_user_blocked" });
        }
    }
}
