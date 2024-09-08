using ArtistApplication.Domain.Domain;
using ArtistApplication.Repository.Interface;
using ArtistApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public void CreateNewGenre(Genre genre)
        {
            _genreRepository.Insert(genre);
        }

        public void DeleteGenre(Guid id)
        {
            var genre = _genreRepository.Get(id);
            _genreRepository.Delete(genre);
        }

        public Genre GetGenreById(Guid? id)
        {
            return _genreRepository.Get(id);
        }

        public List<Genre> GetGenres()
        {
            return _genreRepository.GetAll().ToList();
        }

        public void UpdateGenre(Genre genre)
        {
            _genreRepository.Update(genre);
        }
    }
}
