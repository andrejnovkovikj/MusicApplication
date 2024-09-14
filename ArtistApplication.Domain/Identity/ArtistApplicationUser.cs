using ArtistApplication.Domain.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Identity
{
    public class ArtistApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
        public ICollection<LikedSong>? LikedSongs { get; set; }

    }
}
