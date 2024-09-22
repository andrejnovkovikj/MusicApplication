using ArtistApplication.Domain.Domain;
using ArtistApplication.Domain.ViewModel;
using ArtistApplication.Repository.Interface;
using ArtistApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Song> _songRepository;
        private readonly IRepository<PlaylistSong> _playlistSongRepository;
        
        private const int RandomItemCount = 4;
        public PlaylistService(
            IRepository<Playlist> playlistRepository,
            IRepository<Song> songRepository,
            IRepository<PlaylistSong> playlistSongRepository)
        {
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
            _playlistSongRepository = playlistSongRepository;
        }
        public  List<Playlist> GetRandomPlaylists()
        {
            var playlists =  _playlistRepository.GetAll()
                .OrderBy(r => Guid.NewGuid())
                .Take(RandomItemCount)
                .ToList();
            return playlists;
        }


        public IEnumerable<SongViewModel> GetSongsByPlaylistId(Guid playlistId)
        {
            var playlistSongs = _playlistSongRepository.GetAll()
                .Where(ps => ps.PlaylistId == playlistId)
                .ToList();

            var songIds = playlistSongs.Select(ps => ps.SongId).ToList();
            var songs = _songRepository.GetAll()
                .Where(s => songIds.Contains(s.Id))
                .ToList();

            var songViewModels = songs.Select(song => new SongViewModel
            {
                Song = song
            }).ToList();

            return songViewModels;
        }


        public void AddSongToPlaylist(Guid? playlistId, Guid songId)
        {
            if (playlistId == null)
            {
                throw new ArgumentException("Playlist ID cannot be null");
            }

            var playlist = _playlistRepository.Get(playlistId.Value);
            if (playlist == null)
            {
                throw new ArgumentException("Playlist does not exist");
            }

            var song = _songRepository.Get(songId);
            if (song == null)
            {
                throw new ArgumentException("Song does not exist");
            }

            var existingPlaylistSong = _playlistSongRepository.GetAll()
                .FirstOrDefault(ps => ps.PlaylistId == playlistId.Value && ps.SongId == songId);

            if (existingPlaylistSong == null)
            {
                _playlistSongRepository.Insert(new PlaylistSong
                {
                    PlaylistId = playlistId.Value, 
                    SongId = songId
                });
            }
        }

        public void CreateNewPlaylist(Playlist playlist)
        {
            _playlistRepository.Insert(playlist);
        }

        public void DeletePlaylist(Guid id)
        {
            var playlist = _playlistRepository.Get(id);
            if (playlist != null)
            {
                _playlistRepository.Delete(playlist);
            }
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
