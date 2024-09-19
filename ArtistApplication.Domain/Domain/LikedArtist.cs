using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class LikedArtist : BaseEntity
    {
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string UserId { get; set; }
        public ArtistApplicationUser User { get; set; }
    }
}
