using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class Album : BaseEntity
    {

        public string? Title { get; set; }
        public Guid ArtistId { get; set; }
        public Artist? Artist { get; set; }
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
        public ICollection<Song>? Songs { get; set; }
        public string? PictureUrl { get; set; }
        public int Year { get; set; }

    }
}
