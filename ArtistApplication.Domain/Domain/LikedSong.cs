using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class LikedSong : BaseEntity
    {
        public Guid SongId { get; set; }
        public Song Song { get; set; }
        public string UserId { get; set; }
        public ArtistApplicationUser User { get; set; }
    }
}
