using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IAlbumService
    {
        List<Album> GetRandomAlbums();
        List<Album> GetAlbums();
        Album GetAlbumById(Guid? id);
        void CreateNewAlbum(Album album);
        void UpdateAlbum(Album album);
        void DeleteAlbum(Guid id);
    }
}
