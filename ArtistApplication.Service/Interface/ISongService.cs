using ArtistApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface ISongService
    {
        List<Song> GetSongs();
        Song GetSongById(Guid? id);
        void CreateNewSong(Song song);
        void UpdateSong(Song song);
        void DeleteSong(Guid id);
    }
}
