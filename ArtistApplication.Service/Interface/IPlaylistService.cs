using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IPlaylistService
    {
        List<Playlist> GetPlaylists();
        Playlist GetPlaylistById(Guid? id);
        void CreateNewPlaylist(Playlist playlist);
        void UpdatePlaylist(Playlist playlist);
        void DeletePlaylist(Guid id);
    }
}
