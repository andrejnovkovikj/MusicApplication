using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Domain.Domain
{
    public class Artist : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
        public string? PictureUrl { get; set; }
    }
}
