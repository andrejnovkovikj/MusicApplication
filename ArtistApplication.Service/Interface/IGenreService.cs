using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IGenreService
    {
        List<Genre> GetGenres();
        Genre GetGenreById(Guid? id);
        void CreateNewGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(Guid id);
    }
}
