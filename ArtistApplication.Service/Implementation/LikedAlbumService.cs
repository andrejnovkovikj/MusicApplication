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
    public class LikedAlbumService : ILikedAlbumService
    {
        private readonly IRepository<LikedAlbum> _likedAlbumRepository;
        public LikedAlbumService(IRepository<LikedAlbum> likedAlbumRepository)
        {
            _likedAlbumRepository = likedAlbumRepository;
        }

        public IEnumerable<Album> GetLikedAlbumsByUser(string userId)
        {
            var likedAlbums = _likedAlbumRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => x.Album)
                .ToList();
            return likedAlbums;
        }

        public bool IsAlbumLiked(string userId, Guid albumId)
        {
            return _likedAlbumRepository.GetAll().Any(x => x.UserId == userId && x.AlbumId == albumId);
        }

        public void LikeAlbum(string userId, Guid albumId)
        {
            var likedAlbum = _likedAlbumRepository.GetAll().FirstOrDefault(x => x.UserId == userId && x.AlbumId == albumId);
            if (likedAlbum == null)
            {
                var newLikedAlbum = new LikedAlbum
                {
                    UserId = userId,
                    AlbumId = albumId
                };
                _likedAlbumRepository.Insert(newLikedAlbum);
            }
        }

        public void UnlikeAlbum(string userId, Guid albumId)
        {
            var likedAlbum = _likedAlbumRepository.GetAll().FirstOrDefault(x => x.UserId == userId && x.AlbumId == albumId);
            if(likedAlbum != null)
            {
                _likedAlbumRepository.Delete(likedAlbum);
            }
        }
    }
}
