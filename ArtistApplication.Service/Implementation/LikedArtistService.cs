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
    public class LikedArtistService : ILikedArtistService
    {
        private readonly IRepository<LikedArtist> _likedArtistRepository;

        public LikedArtistService(IRepository<LikedArtist> likedArtistRepository)
        {
            _likedArtistRepository = likedArtistRepository;
        }

        public IEnumerable<Artist> GetLikedArtistsByUser(string userId)
        {
            var likedArtists = _likedArtistRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => x.Artist)
                .ToList();
            return likedArtists;
        }

        public bool IsArtistLiked(string userId, Guid artistId)
        {
            return _likedArtistRepository.GetAll().Any(x => x.UserId == userId && x.ArtistId == artistId);
        }

        public void LikeArtist(string userId, Guid artistId)
        {
            var likedArtist = _likedArtistRepository.GetAll().FirstOrDefault(x => x.UserId == userId && x.ArtistId == artistId);
            if (likedArtist == null)
            {
                var newLikedArtist = new LikedArtist
                {
                    UserId = userId,
                    ArtistId = artistId
                };
                _likedArtistRepository.Insert(newLikedArtist);
            }
        }

        public void UnlikeArtist(string userId, Guid artistId)
        {
            var likedArtist = _likedArtistRepository.GetAll().FirstOrDefault(x => x.UserId == userId && x.ArtistId == artistId);
            if(likedArtist != null)
            {
                _likedArtistRepository.Delete(likedArtist);
            }
        }
    }
}
