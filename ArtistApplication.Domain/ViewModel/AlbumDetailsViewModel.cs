using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.ViewModel
{
    public class AlbumDetailsViewModel
    {
        public Album? Album { get; set; }
        public Artist? Artist { get; set; }
        public Genre? Genre { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();

        public HashSet<Guid> LikedSongIds { get; set; } = new HashSet<Guid>();
    }
}
