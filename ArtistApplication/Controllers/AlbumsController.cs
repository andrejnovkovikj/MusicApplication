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

namespace ArtistApplication.Web.Controllers
{
    
        public class AlbumsController : Controller
        {
            private readonly IAlbumService _albumService;
            private readonly IArtistService _artistService;
            private readonly IGenreService _genreService;
            private readonly ISongService _songService;

            public AlbumsController(IAlbumService albumService, IArtistService artistService, IGenreService genreService, ISongService songService)
            {
                _albumService = albumService;
                _artistService = artistService;
                _genreService = genreService;
                _songService = songService;
            }

            // GET: Albums
            public IActionResult Index()
            {
                var albums = _albumService.GetAlbums();
                return View(albums);
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

            var allSongs = _songService.GetSongs(); 
            var songs = allSongs.Where(s => s.AlbumId == id).ToList(); 

            var viewModel = new AlbumDetailsViewModel
            {
                Album = album,
                Artist = artist,
                Genre = genre,
                Songs = songs
            };

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
