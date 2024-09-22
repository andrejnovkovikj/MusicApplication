using ArtistApplication.Domain.Domain;
using ArtistApplication.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IPlaylistService
    {
        List<Playlist> GetRandomPlaylists();
        void AddSongToPlaylist(Guid? playlistId, Guid songId);
        List<Playlist> GetPlaylists();
        Playlist GetPlaylistById(Guid? id);
        void CreateNewPlaylist(Playlist playlist);
        void UpdatePlaylist(Playlist playlist);
        void DeletePlaylist(Guid id);
        IEnumerable<SongViewModel> GetSongsByPlaylistId(Guid playlistId);
    }
}
