using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GL.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class GamesRatingsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ratings_RatingId1",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Games_GameId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Games_RatingId1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Ratings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Games",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Studio",
                table: "Games",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_RatingId",
                table: "Games",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ratings_RatingId",
                table: "Games",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Games_GameId",
                table: "Ratings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ratings_RatingId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Games_GameId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Games_RatingId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Studio",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingId1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_RatingId1",
                table: "Games",
                column: "RatingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ratings_RatingId1",
                table: "Games",
                column: "RatingId1",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Games_GameId",
                table: "Ratings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
