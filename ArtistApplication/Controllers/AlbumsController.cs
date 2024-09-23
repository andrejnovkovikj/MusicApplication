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
    
        public class AlbumsController : Controller
        {
            private readonly IAlbumService _albumService;
            private readonly IArtistService _artistService;
            private readonly IGenreService _genreService;
            private readonly ISongService _songService;
            private readonly ILikedSongService _likedSongService;
            private readonly ILikedAlbumService _likedAlbumService;
            private readonly IPlaylistService _playlistService;

            public AlbumsController(IAlbumService albumService, IArtistService artistService, IGenreService genreService, ISongService songService,ILikedSongService likedSongService,ILikedAlbumService likedAlbumService,IPlaylistService playlistService)
            {
                _albumService = albumService;
                _artistService = artistService;
                _genreService = genreService;
                _songService = songService;
                _likedSongService = likedSongService;
                _likedAlbumService = likedAlbumService;
                _playlistService = playlistService;
            }
        // LikedAlbums part//

        [HttpPost]
        public IActionResult Like(Guid albumId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedAlbumService.LikeAlbum(userId, albumId);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Unlike(Guid albumId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedAlbumService.UnlikeAlbum(userId, albumId);
            }

            return RedirectToAction("Index"); // Redirect to avoid form resubmission
        }

        [HttpGet]
        public IActionResult MyLikedAlbums()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var likedAlbums = _likedAlbumService.GetLikedAlbumsByUser(userId);

                ViewBag.likedAlbums = likedAlbums.Select(ls => ls.Id).ToList();

                return View(likedAlbums);
            }

            return RedirectToAction("Index");
        }



        // end of LikedAlbums part//




        // GET: Albums
        public IActionResult Index(string? searchString)
        {
            var albums = _albumService.GetAlbums(); 
            var artists = _artistService.GetArtists(); 
            var genres = _genreService.GetGenres();

            var artistsFilter = artists
                .Where(a => a.Name != null && a.Name.Contains(searchString ?? "", StringComparison.OrdinalIgnoreCase))
                .ToList();
            var genresFilter = genres
                .Where(a => a.Name != null && a.Name.Contains(searchString ?? "", StringComparison.OrdinalIgnoreCase))
                .ToList();


            var artistsFilteredIds = artistsFilter.Select(a => a.Id).ToList();
            var genresFiltererIds = genresFilter.Select(a => a.Id).ToList();

            var filteredAlbums = albums
                .Where(a => (a.ArtistId != null && artistsFilteredIds.Contains(a.ArtistId))
                     || (a.GenreId != null && genresFiltererIds.Contains(a.GenreId))
                     || (a.Title != null && a.Title.Contains(searchString)))
                .ToList();

            ViewBag.SearchString = searchString;
            ViewBag.NoDataMessage = filteredAlbums.Any() ? null : "No albums found.";
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var likedAlbums = _likedAlbumService.GetLikedAlbumsByUser(userId);
                ViewBag.LikedAlbums = likedAlbums.Select(p => p.Id).ToList();
            }
            else
            {
                ViewBag.LikedAlbums = new List<Guid>();
            }

            return View(filteredAlbums);
        }




        // GET: Albums/Details/5
        public IActionResult Details(Guid id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(album.ArtistId);
            var genre = _genreService.GetGenreById(album.GenreId);
            var playlists = _playlistService.GetPlaylists();
            var allSongs = _songService.GetSongs();
            var songs = allSongs.Where(s => s.AlbumId == id).ToList();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var likedSongIds = userId != null ? _likedSongService.GetLikedSongsByUser(userId).Select(ls => ls.Id).ToHashSet() : new HashSet<Guid>();

            var viewModel = new AlbumDetailsViewModel
            {
                Album = album,
                Artist = artist,
                Genre = genre,
                Songs = songs,
                LikedSongIds = likedSongIds
            };
            ViewBag.Playlists = playlists;
            return View(viewModel);
        }

        // GET: Albums/Create
        public IActionResult Create()
            {
                ViewData["ArtistId"] = new SelectList(_artistService.GetArtists(), "Id", "Name");
                ViewData["GenreId"] = new SelectList(_genreService.GetGenres(), "Id", "Name");
                return View();
            }

            // POST: Albums/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("Id,Title,ArtistId,GenreId,PictureUrl,Year")] Album album)
            {
                if (ModelState.IsValid)
                {
                    album.Id = Guid.NewGuid();
                    _albumService.CreateNewAlbum(album);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ArtistId"] = new SelectList(_artistService.GetArtists(), "Id", "Name");
                ViewData["GenreId"] = new SelectList(_genreService.GetGenres(), "Id", "Name");
                return View(album);
            }

            // GET: Albums/Edit/5
            public IActionResult Edit(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var album = _albumService.GetAlbumById(id);
                if (album == null)
                {
                    return NotFound();
                }
                ViewData["ArtistId"] = new SelectList(_artistService.GetArtists(), "Id", "Name");
                ViewData["GenreId"] = new SelectList(_genreService.GetGenres(), "Id", "Name");
                return View(album);
            }

            // POST: Albums/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Guid id, [Bind("Id,Title,ArtistId,GenreId,PictureUrl,Year")] Album album)
            {
                if (id != album.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _albumService.UpdateAlbum(album);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ArtistId"] = new SelectList(_artistService.GetArtists(), "Id", "Name");
                ViewData["GenreId"] = new SelectList(_genreService.GetGenres(), "Id", "Name");
                return View(album);
            }

            // GET: Albums/Delete/5
            public IActionResult Delete(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var album = _albumService.GetAlbumById(id);
                if (album == null)
                {
                    return NotFound();
                }

                return View(album);
            }

            // POST: Albums/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(Guid id)
            {


                _albumService.DeleteAlbum(id);
                return RedirectToAction(nameof(Index));

            }

        }
    }
