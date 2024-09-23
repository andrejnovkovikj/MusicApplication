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
    public class ArtistsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;
        private readonly ISongService _songService;
        private readonly ILikedArtistService _likedArtistService;

        public ArtistsController(IAlbumService albumService, IArtistService artistService, IGenreService genreService, ISongService songService, ILikedArtistService likedArtistService)
        {
            _albumService = albumService;
            _artistService = artistService;
            _genreService = genreService;
            _songService = songService;
            _likedArtistService = likedArtistService;
        }
        // LikedArtist part//
        [HttpPost]
        public IActionResult Like(Guid artistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedArtistService.LikeArtist(userId, artistId);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Unlike(Guid artistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedArtistService.UnlikeArtist(userId, artistId);
            }

            return RedirectToAction("Index"); // Redirect to avoid form resubmission
        }

        [HttpGet]
        public IActionResult MyLikedArtists()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var likedArtists = _likedArtistService.GetLikedArtistsByUser(userId);

                ViewBag.likedArtists = likedArtists.Select(ls => ls.Id).ToList();

                return View(likedArtists);
            }

            return RedirectToAction("Index");
        }



        // end of LikedArtist part//

        // GET: Artists
        public IActionResult Index(string? searchString)
        {
            var artists = _artistService.GetArtists();

            var searchLower = searchString?.ToLower() ?? string.Empty;

            var filteredArtists = artists
                .Where(a => !string.IsNullOrEmpty(a.Name) && a.Name.ToLower().Contains(searchLower))
                .ToList();

            ViewBag.SearchString = searchString;
            ViewBag.NoDataMessage = filteredArtists.Any() ? null : "No artists found.";

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var likedArtists = _likedArtistService.GetLikedArtistsByUser(userId);
                ViewBag.likedArtists = likedArtists.Select(p => p.Id).ToList();
            }
            else
            {
                ViewBag.LikedArtists = new List<Guid>();
            }

            return View(filteredArtists);
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            var albums = _albumService.GetAlbums().Where(s => s.ArtistId == id).ToList();

            var viewModel = new ArtistDetailsViewModel
            {
                Artist = artist,
                Albums = albums
            };

            return View(viewModel);
        }


        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,PictureUrl")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                artist.Id = Guid.NewGuid();
                _artistService.CreateNewArtist(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,PictureUrl")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _artistService.UpdateArtist(artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _artistService.DeleteArtist(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
