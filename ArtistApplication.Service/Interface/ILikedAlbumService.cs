using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface ILikedAlbumService
    {
        IEnumerable<Album> GetLikedAlbumsByUser(string userId);
        void LikeAlbum(string userId, Guid albumId);
        void UnlikeAlbum(string userId, Guid albumId);
        bool IsAlbumLiked(string userId, Guid albumId);
    }
}
