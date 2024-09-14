using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface ILikedSongService
    {
        IEnumerable<Song> GetLikedSongsByUser(string userId);
        void LikeSong(string userId, Guid songId);
        void UnlikeSong(string userId, Guid songId);
        bool IsSongLiked(string userId, Guid songId);
    }
}
