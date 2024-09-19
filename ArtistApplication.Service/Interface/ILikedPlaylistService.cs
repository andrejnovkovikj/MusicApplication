using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface ILikedPlaylistService
    {
        IEnumerable<Playlist> GetLikedPlaylistsByUser(string userId);
        void LikePlaylist(string userId, Guid playlistId);
        void UnlikePlaylist(string userId, Guid playlistId);
        bool IsPlaylistLiked(string userId, Guid playlistId);
    }
}
