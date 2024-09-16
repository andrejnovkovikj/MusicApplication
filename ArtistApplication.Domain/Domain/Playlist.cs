using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class Playlist : BaseEntity
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? UserId { get; set; }
        public ArtistApplicationUser? User { get; set; }

        public ICollection<PlaylistSong>? PlaylistSongs { get; set; }
        public string? PictureUrl { get; set; }

    }
}
