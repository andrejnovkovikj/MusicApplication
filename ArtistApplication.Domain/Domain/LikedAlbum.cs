using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class LikedAlbum : BaseEntity
    {
        public Guid AlbumId { get; set; }
        public Album Album { get; set; }
        public string UserId { get; set; }
        public ArtistApplicationUser User { get; set; }
    }
}
