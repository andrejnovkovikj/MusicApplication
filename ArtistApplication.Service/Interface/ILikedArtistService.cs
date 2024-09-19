using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface ILikedArtistService
    {
        IEnumerable<Artist> GetLikedArtistsByUser(string userId);
        void LikeArtist(string userId, Guid artistId);
        void UnlikeArtist(string userId, Guid artistId);
        bool IsArtistLiked(string userId, Guid artistId);
    }
}
