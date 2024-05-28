using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GL.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class GamesRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ratings_RatingId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_RatingId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingId1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_GameId",
                table: "Ratings",
                column: "GameId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ratings_RatingId1",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Games_GameId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_GameId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Games_RatingId1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "Games");

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
        }
    }
}
