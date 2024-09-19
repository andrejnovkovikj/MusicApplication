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
using System.Security.Claims;
using ArtistApplication.Service.Implementation;
using ArtistApplication.Domain.ViewModel;
using ArtistApplication.Domain;

namespace ArtistApplication.Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IUserService _userService;
        private readonly IAlbumService _albumService;
        private readonly ILikedSongService _likedSongService;
        private readonly ILikedPlaylistService _likedPlaylistService;

        public PlaylistsController(IPlaylistService playlistService, IUserService userService, IAlbumService albumService, ILikedSongService likedSongService, ILikedPlaylistService likedPlaylistService)
        {
            _playlistService = playlistService;
            _userService = userService;
            _albumService = albumService;
            _likedSongService = likedSongService;
            _likedPlaylistService = likedPlaylistService;
        }

        // LikedPlaylist part//

        [HttpPost]
        public IActionResult Like(Guid playlistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedPlaylistService.LikePlaylist(userId, playlistId);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Unlike(Guid playlistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _likedPlaylistService.UnlikePlaylist(userId, playlistId);
            }

            return RedirectToAction("Index"); // Redirect to avoid form resubmission
        }

        [HttpGet]
        public IActionResult MyLikedPlaylists()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var likedPlaylists = _likedPlaylistService.GetLikedPlaylistsByUser(userId);

                ViewBag.likedPlaylists = likedPlaylists.Select(ls => ls.Id).ToList();

                return View(likedPlaylists);
            }

            return RedirectToAction("Index");
        }



        // end of LikedPlaylist part//





        // GET: Playlists
        public IActionResult Index(string searchString)
        {
            var playlists = _playlistService.GetPlaylists();

            if (!string.IsNullOrEmpty(searchString))
            {
                playlists = playlists
                    .Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var userIds = playlists.Select(p => p.UserId).Distinct();
            var users = _userService.GetUsersByIds(userIds);
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            var model = playlists.Select(p => new PlaylistViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                UserName = userDictionary.ContainsKey(p.UserId) ? userDictionary[p.UserId] : "Unknown",
                PictureUrl=p.PictureUrl
            }).ToList();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var likedPlaylists = _likedPlaylistService.GetLikedPlaylistsByUser(userId);
                ViewBag.LikedPlaylists = likedPlaylists.Select(p => p.Id).ToList();
            }
            else
            {
                ViewBag.LikedPlaylists = new List<Guid>();
            }
            ViewBag.SearchString = searchString; 
            return View(model);
        }

       
        // GET: Playlists/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var likedSongs = new List<Guid>();
            if (userId != null)
            {
                likedSongs = _likedSongService.GetLikedSongsByUser(userId).Select(s => s.Id).ToList();
            }
            ViewBag.LikedSongs = likedSongs;



            var playlist = _playlistService.GetPlaylistById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var userName = _userService.GetUsernameById(playlist.UserId);

            var songs = _playlistService.GetSongsByPlaylistId(id);

            var albumIds = songs.Select(s => s.Song.Album?.Id).Where(id => id != null).Distinct().ToList();

            var albums = _albumService.GetAlbums()
                .Where(a => albumIds.Contains(a.Id))
                .ToList();

            var albumDictionary = albums.ToDictionary(a => a.Id);

            var model = new PlaylistViewModel
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                UserName = userName,
                PictureUrl=playlist.PictureUrl,
                Songs = songs.Select(song => new SongViewModel
                {
                    Song = song.Song,
                    Album = song.Song.Album != null && albumDictionary.ContainsKey(song.Song.Album.Id) ? albumDictionary[song.Song.Album.Id] : null
                }),
            };

            return View(model);
        }


        // GET: Playlists/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new PlaylistViewModel
            {
                UserName = _userService.GetUsernameById(userId)
            };
            return View(model);
        }

        // POST: Playlists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,PictureUrl")] PlaylistViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var playlist = new Playlist
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    PictureUrl = model.PictureUrl,
                    UserId = userId
                };

                _playlistService.CreateNewPlaylist(playlist);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = _playlistService.GetPlaylistById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var userName = _userService.GetUsernameById(playlist.UserId);
            var model = new PlaylistViewModel
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                UserName = userName
            };

            return View(model);
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,UserName,PictureUrl,Id")] PlaylistViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var playlist = new Playlist
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    PictureUrl = model.PictureUrl,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _playlistService.UpdatePlaylist(playlist);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = _playlistService.GetPlaylistById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var userName = _userService.GetUsernameById(playlist.UserId);
            var model = new PlaylistViewModel
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                UserName = userName
            };

            return View(model);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _playlistService.DeletePlaylist(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
