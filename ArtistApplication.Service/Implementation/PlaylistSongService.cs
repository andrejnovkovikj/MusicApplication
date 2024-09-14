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
    public class PlaylistSongService : IPlaylistSongService
    {
        private readonly IRepository<PlaylistSong> _playlistSongRepository;
        public PlaylistSongService(IRepository<PlaylistSong> playlistSongRepository)
        {
            _playlistSongRepository = playlistSongRepository;
        }

        public void CreateNewPlaylistSong(PlaylistSong playlistSong)
        {
            _playlistSongRepository.Insert(playlistSong);
        }

        public void DeletePlaylistSong(Guid id)
        {
            var playlistSong = _playlistSongRepository.Get(id);
            _playlistSongRepository.Delete(playlistSong);
        }

        public PlaylistSong GetPlaylistSongById(Guid? id)
        {
            return _playlistSongRepository.Get(id);
        }

        public List<PlaylistSong> GetPlaylistSongs()
        {
            return _playlistSongRepository.GetAll().ToList();
        }

        public void UpdatePlaylistSong(PlaylistSong playlistSong)
        {
            _playlistSongRepository.Update(playlistSong);
        }
    }
}
