using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtistApplication.Domain.Domain;
using ArtistApplication.Repository;
using ArtistApplication.Service.Interface;
using ArtistApplication.Domain.ViewModel;
using ArtistApplication.Service.Implementation;
using System.Security.Claims;

namespace ArtistApplication.Web.Controllers
{
    public class SongsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;
        private readonly ISongService _songService;
        private readonly ILikedSongService _likedSongService;

        public SongsController(IAlbumService albumService, IArtistService artistService, IGenreService genreService, ISongService songService, ILikedSongService likedSongService)
        {
            _albumService = albumService;
            _artistService = artistService;
            _genreService = genreService;
            _songService = songService;
            _likedSongService = likedSongService;
        }

        [HttpPost]
        public IActionResult Like(Guid songId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedSongService.LikeSong(userId, songId);
            }

            return RedirectToAction("MyLikedSongs");
        }

        [HttpPost]
        public IActionResult Unlike(Guid songId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedSongService.UnlikeSong(userId, songId);
            }
            return RedirectToAction("MyLikedSongs");
        }

        [HttpGet]
        public IActionResult MyLikedSongs()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                // Fetch the liked songs for the user
                var likedSongs = _likedSongService.GetLikedSongsByUser(userId);

                // Prepare a list of LikedSongsViewModel
                var viewModel = new List<LikedSongsViewModel>();

                foreach (var song in likedSongs)
                {
                    // Assuming you have methods to get the related entities
                    var album = _albumService.GetAlbumById(song.AlbumId);
                    var artist = _artistService.GetArtistById(album?.ArtistId ?? Guid.Empty);
                    var genre = _genreService.GetGenreById(album?.GenreId ?? Guid.Empty);

                    viewModel.Add(new LikedSongsViewModel
                    {
                        Song = song,
                        Album = album,
                        Artist = artist,
                        Genre = genre
                    });
                }

                // Populate ViewBag with the list of liked song IDs
                ViewBag.LikedSongs = likedSongs.Select(ls => ls.Id).ToList();

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }


        // GET: Songs
        public IActionResult Index(string searchString)
        {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var songs = _songService.GetSongs();

                // Get albums for the songs
                var albums = _albumService.GetAlbums(); // Assuming you have a method to get all albums

                // Filter songs by search string
                if (!string.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    songs = songs.Where(s => s.Title.ToLower().Contains(searchString)).ToList();
                }

                // Get the list of liked songs for the current user
                var likedSongs = new List<Guid>();
                if (userId != null)
                {
                    likedSongs = _likedSongService.GetLikedSongsByUser(userId).Select(s => s.Id).ToList();
                }

                // Prepare the view model
                var viewModel = songs.Select(song => new SongViewModel
                {
                    Song = song,
                    Album = albums.FirstOrDefault(album => album.Id == song.AlbumId) // Fetch the related album
                }).ToList();

                ViewBag.LikedSongs = likedSongs;
                ViewBag.SearchString = searchString;

                return View(viewModel);
            
        }

        // GET: Songs/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = _songService.GetSongById(id);
            var albumId = song.AlbumId;
            var album = _albumService.GetAlbumById(albumId);
            var artistId = album.ArtistId;
            var artist = _artistService.GetArtistById(artistId);
            var genreId = album.GenreId;
            var genre = _genreService.GetGenreById(genreId);
            if (song == null)
            {
                return NotFound();
            }

            var viewModel = new SongDetailsViewModel
            {
                Song = song,
                Album = album,
                Artist = artist,
                Genre = genre,
            };

            return View(viewModel);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_albumService.GetAlbums(), "Id", "Title");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,AlbumId,Length")] Song song, IFormFile audioFile)
        {
            if (ModelState.IsValid)
            {
                song.Id = Guid.NewGuid();

                if (audioFile != null && audioFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}_{audioFile.FileName}";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        audioFile.CopyTo(stream);
                    }

                    song.FileName = fileName;
                    
                }

                _songService.CreateNewSong(song);

                return RedirectToAction(nameof(Index));
            }

            ViewData["AlbumId"] = new SelectList(_albumService.GetAlbums(), "Id", "Title", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_albumService.GetAlbums(), "Id", "Title", song.AlbumId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(Guid id, [Bind("Id,Title,AlbumId,Length,FileName")] Song song, IFormFile? audioFile)
{
    if (id != song.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Handle the file upload if a new audio file is provided
            if (audioFile != null && audioFile.Length > 0)
            {
                // Generate a unique file name
                var fileName = $"{Guid.NewGuid()}_{audioFile.FileName}";

                // Specify the path where the file will be saved
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    audioFile.CopyTo(stream);
                }

                // Update the song's file name with the new file name
                song.FileName = fileName;
            }

            // Update the song entity using the service
            _songService.UpdateSong(song);

            // Redirect to the index action after successful update
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle the concurrency exception here
            // You can log the exception or display an error message
            // For simplicity, just rethrowing it
            throw;
        }
        catch (Exception ex)
        {
            // Handle other potential exceptions
            // Log the exception or display a friendly error message
            ModelState.AddModelError(string.Empty, "An error occurred while updating the song.");
            // Optionally, you can log the exception details here
        }
    }

    ViewData["AlbumId"] = new SelectList(_albumService.GetAlbums(), "Id", "Title", song.AlbumId);
    return View(song);
}

        // GET: Songs/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _songService.DeleteSong(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
