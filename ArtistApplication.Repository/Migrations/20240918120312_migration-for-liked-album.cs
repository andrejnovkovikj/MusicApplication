using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistApplication.Repository.Migrations
{
    /// <inheritdoc />
    public partial class migrationforlikedalbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedAlbums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedAlbums_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedArtists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedArtists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedArtists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedArtists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedPlaylists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedPlaylists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedPlaylists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedPlaylists_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedAlbums_AlbumId",
                table: "LikedAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedAlbums_UserId",
                table: "LikedAlbums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedArtists_ArtistId",
                table: "LikedArtists",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedArtists_UserId",
                table: "LikedArtists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedPlaylists_PlaylistId",
                table: "LikedPlaylists",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedPlaylists_UserId",
                table: "LikedPlaylists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedAlbums");

            migrationBuilder.DropTable(
                name: "LikedArtists");

            migrationBuilder.DropTable(
                name: "LikedPlaylists");
        }
    }
}
