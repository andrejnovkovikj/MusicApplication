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
    public class AlbumService : IAlbumService
    {

        private readonly IRepository<Album> _albumRepository;
        private const int RandomItemCount = 3;
        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public  List<Album> GetRandomAlbums()
        {
            var albums = _albumRepository.GetAll()
                .OrderBy(r => Guid.NewGuid())
                .Take(RandomItemCount)
                .ToList();
            return albums;
        }
        public void CreateNewAlbum(Album album)
        {
            _albumRepository.Insert(album);
        }

        public void DeleteAlbum(Guid id)
        {
            var album = _albumRepository.Get(id);
            _albumRepository.Delete(album);
        }

        public Album GetAlbumById(Guid? id)
        {
            return _albumRepository.Get(id);
        }

        public List<Album> GetAlbums()
        {
            return _albumRepository.GetAll().ToList();
        }

        public void UpdateAlbum(Album album)
        {
            _albumRepository.Update(album);
        }
    }
}
