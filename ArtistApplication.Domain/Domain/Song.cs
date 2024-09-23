using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class Song : BaseEntity
    {
        public string? Title { get; set; }
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }
        public TimeSpan Length { get; set; }
        public string? FileName { get; set; }
        public virtual ICollection<PlaylistSong>? PlaylistSongs { get; set; } 

    }
}
