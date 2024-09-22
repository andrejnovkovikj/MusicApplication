using ArtistApplication.Domain;
using ArtistApplication.Domain.Domain;
using ArtistApplication.Domain.ViewModel;
using ArtistApplication.Service.Implementation;
using ArtistApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ArtistApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumService _albumService;
        private readonly ISongService _songService;
        private readonly IArtistService _artistService;
        private readonly IPlaylistService _playlistService;
        private readonly IGenreService _genreService;
        private readonly ILikedSongService _likedSongService;

        public HomeController(ILogger<HomeController> logger, IAlbumService albumService,ISongService songService,IArtistService artistService,IPlaylistService playlistService,IGenreService genreService,ILikedSongService likedSongService)
        {
            _logger = logger;
            _albumService = albumService;
            _songService = songService;
            _artistService = artistService;
            _playlistService = playlistService;
            _genreService = genreService;
            _likedSongService = likedSongService;

        }

        public IActionResult Index()
        {
            var randomAlbums =  _albumService.GetRandomAlbums();
            var allAlbums = _albumService.GetAlbums();
            var allGenres = _genreService.GetGenres();
            var allArtists = _artistService.GetArtists();

           /* var viewModel = songs.Select(song => new SongViewModel
            {
                Song = song,
                Album = albums.FirstOrDefault(album => album.Id == song.AlbumId)
            }).ToList(); */


            var randomSongs =  _songService.GetRandomSongs();
            var viewModel = randomSongs.Select(randomSong => new SongDetailsViewModel
            {
                Song = randomSong,
                Album = allAlbums.FirstOrDefault(album => album.Id == randomSong.AlbumId),
                Genre = allGenres.FirstOrDefault(genre => genre.Id == randomSong.Album.GenreId),
                Artist = allArtists.FirstOrDefault(artist => artist.Id == randomSong.Album.ArtistId)

            }).ToList();

            var randomArtists =  _artistService.GetRandomArtists();
            var randomPlaylists =  _playlistService.GetRandomPlaylists();

            var model = new HomeViewModel
            {
                RecommendedAlbums = randomAlbums,


                RecommendedSongs = viewModel,


                RecommendedArtists = randomArtists,
                RecommendedPlaylists = randomPlaylists
            };
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var likedSongs = new List<Guid>();
            if (userId != null)
            {
                likedSongs = _likedSongService.GetLikedSongsByUser(userId).Select(s => s.Id).ToList();
            }
            ViewBag.LikedSongs = likedSongs;



            return View(model);
        }
        public IActionResult Browse()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
