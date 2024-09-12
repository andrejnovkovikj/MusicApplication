﻿using ArtistApplication.Domain.Domain;
using ArtistApplication.Repository.Interface;
using ArtistApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Implementation
{
    public class SongService : ISongService
    {
        private readonly IRepository<Song> _songRepository;
        public SongService(IRepository<Song> songRepository)
        {
            _songRepository = songRepository;
        }

        public void CreateNewSong(Song song)
        {
            _songRepository.Insert(song);
        }

        public void DeleteSong(Guid id)
        {
            var song = _songRepository.Get(id);
            _songRepository.Delete(song);
        }

        public Song GetSongById(Guid? id)
        {
            return _songRepository.Get(id);
        }

        public List<Song> GetSongs()
        {
            return _songRepository.GetAll().ToList();
        }

        public void UpdateSong(Song song)
        {
            _songRepository.Update(song);
        }
    }
}