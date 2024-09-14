using ArtistApplication.Domain.Domain;
using ArtistApplication.Repository.Interface;
using ArtistApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Implementation
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist> _playlistRepository;

        public PlaylistService(IRepository<Playlist> playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public void CreateNewPlaylist(Playlist playlist)
        {
           _playlistRepository.Insert(playlist);
        }

        public void DeletePlaylist(Guid id)
        {
            var playlist = _playlistRepository.Get(id);
            _playlistRepository.Delete(playlist);
        }

        public Playlist GetPlaylistById(Guid? id)
        {
            return _playlistRepository.Get(id);
        }

        public List<Playlist> GetPlaylists()
        {
            return _playlistRepository.GetAll().ToList();
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            _playlistRepository.Update(playlist);
        }
    }
}
