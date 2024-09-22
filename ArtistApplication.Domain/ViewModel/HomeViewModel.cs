using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.ViewModel
{
    public class HomeViewModel
    {
        public List<SongDetailsViewModel> RecommendedSongs { get; set; }
        public List<Album> RecommendedAlbums { get; set; }
        public List<Playlist> RecommendedPlaylists { get; set; }
        public List<Artist> RecommendedArtists { get; set; }
    }

}
