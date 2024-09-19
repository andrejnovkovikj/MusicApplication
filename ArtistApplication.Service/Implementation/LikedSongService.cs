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
    public class LikedSongService : ILikedSongService
    {
        private readonly IRepository<LikedSong> _likedSongRepository;

        public LikedSongService(IRepository<LikedSong> likedSongRepository)
        {
            _likedSongRepository = likedSongRepository;
        }

        public IEnumerable<Song> GetLikedSongsByUser(string userId)
        {
            var likedSongs = _likedSongRepository.GetAll()
                .Where(ls => ls.UserId == userId)
                .Select(ls => ls.Song)
                .ToList();

            return likedSongs;
        }

        public void LikeSong(string userId, Guid songId)
        {
            var likedSong = _likedSongRepository.GetAll().FirstOrDefault(ls => ls.UserId == userId && ls.SongId == songId);

            if (likedSong == null)
            {
                var newLikedSong = new LikedSong
                {
                    UserId = userId,
                    SongId = songId
                };

                _likedSongRepository.Insert(newLikedSong);
            }
        }

        public void UnlikeSong(string userId, Guid songId)
        {
            var likedSong = _likedSongRepository.GetAll().FirstOrDefault(ls => ls.UserId == userId && ls.SongId == songId);

            if (likedSong != null)
            {
                _likedSongRepository.Delete(likedSong);
            }
        }

        public bool IsSongLiked(string userId, Guid songId)
        {
            return _likedSongRepository.GetAll().Any(ls => ls.UserId == userId && ls.SongId == songId);
        }
    }
}
