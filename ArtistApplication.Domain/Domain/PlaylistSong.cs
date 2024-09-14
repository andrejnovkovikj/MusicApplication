using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class PlaylistSong : BaseEntity
    {

        public Guid PlaylistId { get; set; }
        public Playlist? Playlist { get; set; }

        public Guid SongId { get; set; }
        public Song? Song { get; set; }
    }
}
