using ArtistApplication.Domain.Domain;
using ArtistApplication.Repository.Interface;
using ArtistApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Implementation
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;
        private const int RandomItemCount = 5;
        public List<Artist> GetRandomArtists()
        {
            var artists = _artistRepository.GetAll()
                .OrderBy(r => Guid.NewGuid())
                .Take(RandomItemCount)
                .ToList();
            return artists;
        }
        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void CreateNewArtist(Artist artist)
        {
            _artistRepository.Insert(artist);
        }

        public void DeleteArtist(Guid id)
        {
            var artist = _artistRepository.Get(id);
            _artistRepository.Delete(artist);
        }

        public Artist GetArtistById(Guid? id)
        {
            return _artistRepository.Get(id);
        }

        public List<Artist> GetArtists()
        {
            return _artistRepository.GetAll().ToList();
        }

        public void UpdateArtist(Artist artist)
        {
            _artistRepository.Update(artist);
        }
    }
}
