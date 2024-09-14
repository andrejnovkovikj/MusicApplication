using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.ViewModel
{
    public class LikedSongsViewModel
    {
        public Song? Song { get; set; }
        public Album? Album { get; set; }
        public Artist? Artist { get; set; }
        public Genre? Genre { get; set; }
    }
}
