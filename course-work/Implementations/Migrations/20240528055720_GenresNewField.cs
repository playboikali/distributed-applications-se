using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GL.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class GenresNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewField",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewField",
                table: "Genres");
        }
    }
}
