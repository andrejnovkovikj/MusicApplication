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
    public class LikedPlaylistService : ILikedPlaylistService
    {
        private readonly IRepository<LikedPlaylist> _likedPlaylistRepository;

        public LikedPlaylistService( IRepository<LikedPlaylist> likedPlaylistRepository)
        {
            _likedPlaylistRepository = likedPlaylistRepository;
        }

        public IEnumerable<Playlist> GetLikedPlaylistsByUser(string userId)
        {
            var likedPlaylists = _likedPlaylistRepository.GetAll()
                .Where(lp => lp.UserId == userId)
                .Select(lp => lp.Playlist)
                .ToList();
            return likedPlaylists;
        }
        
        public void LikePlaylist(string userId, Guid playlistId)
        {
           var likedPlaylist = _likedPlaylistRepository.GetAll().FirstOrDefault(lp => lp.UserId == userId && lp.PlaylistId == playlistId);
            if (likedPlaylist == null)
            {
                var newLikedPlaylist = new LikedPlaylist
                {
                    UserId = userId,
                    PlaylistId = playlistId
                };
                _likedPlaylistRepository.Insert(newLikedPlaylist);
            }
        }

        public void UnlikePlaylist(string userId, Guid playlistId)
        {
            var likedPlaylist = _likedPlaylistRepository.GetAll().FirstOrDefault(lp => lp.UserId == userId && lp.PlaylistId == playlistId);
            if(likedPlaylist != null)
            {
                _likedPlaylistRepository.Delete(likedPlaylist);
            }
        }

        public bool IsPlaylistLiked(string userId, Guid playlistId)
        {
            return _likedPlaylistRepository.GetAll().Any(lp => lp.UserId == userId && lp.PlaylistId == playlistId);
        }

    }
}
