using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class LikedPlaylist : BaseEntity
    {
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public string UserId { get; set; }
        public ArtistApplicationUser User { get; set; }
    }
}
