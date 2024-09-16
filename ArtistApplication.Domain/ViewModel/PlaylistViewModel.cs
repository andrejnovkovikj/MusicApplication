using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.ViewModel
{
    public class PlaylistViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; }
        public string? PictureUrl { get; set; }
        public IEnumerable<SongViewModel>? Songs { get; set; }
    }
}
