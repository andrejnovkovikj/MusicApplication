using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IArtistService
    {
        List<Artist> GetArtists();
        Artist GetArtistById(Guid? id);
        void CreateNewArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(Guid id);
    }
}
