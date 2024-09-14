using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.ViewModel
{
    public class ArtistDetailsViewModel
    {
        public Artist? Artist { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();

    }
}
