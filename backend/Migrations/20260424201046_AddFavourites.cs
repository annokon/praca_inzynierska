using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFavourites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourite_user_id_user",
                table: "favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_favourite_user_id_user_favourite",
                table: "favourite");

            migrationBuilder.AlterColumn<int>(
                name: "id_user_favourite",
                table: "favourite",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_user",
                table: "favourite",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_user_id_user",
                table: "favourite",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_user_id_user_favourite",
                table: "favourite",
                column: "id_user_favourite",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourite_user_id_user",
                table: "favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_favourite_user_id_user_favourite",
                table: "favourite");

            migrationBuilder.AlterColumn<int>(
                name: "id_user_favourite",
                table: "favourite",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "id_user",
                table: "favourite",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_user_id_user",
                table: "favourite",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_user_id_user_favourite",
                table: "favourite",
                column: "id_user_favourite",
                principalTable: "user",
                principalColumn: "id_user");
        }
    }
}
