using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IPlaylistSongService
    {
        List<PlaylistSong> GetPlaylistSongs();
        PlaylistSong GetPlaylistSongById(Guid? id);
        void CreateNewPlaylistSong(PlaylistSong playlistSong);
        void UpdatePlaylistSong(PlaylistSong playlistSong);
        void DeletePlaylistSong(Guid id);
    }
}
