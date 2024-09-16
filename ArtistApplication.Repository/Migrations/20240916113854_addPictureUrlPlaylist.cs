using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistApplication.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addPictureUrlPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Playlists");
        }
    }
}
