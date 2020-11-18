using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RidePal.Services.Contracts;
using RidePal.Web.Models;
using System.Linq;

namespace RidePal.Web.Controllers
{
    [Route("search")]
    public class SearchController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly ITrackService _trackService;

        public SearchController(IMapper mapper, IGenreService genreService, IArtistService artistService, IAlbumService albumService, ITrackService trackService)
        {
            _mapper = mapper;
            _genreService = genreService;
            _artistService = artistService;
            _albumService = albumService;
            _trackService = trackService;
        }

        // GET: Search
        //Browse all
        public IActionResult Index()
        {
            var genres = _genreService.GetAllGenres();
            var genresVM = genres.Select(x => _mapper.Map<GenreVM>(x)).ToList();

            return View(genresVM);
        }

        [HttpGet("{searchString}")]
        public IActionResult BrowseAll(string searchString)
        {
            var tracks = _trackService.GetTopTracks(5, searchString: searchString);
            var tracksVM = tracks.Select(x => _mapper.Map<TrackVM>(x)).ToList();

            var artists = _artistService.GetTopArtists(6, searchString: searchString);
            var artistsVM = artists.Select(x => _mapper.Map<ArtistVM>(x)).ToList();

            var albums = _albumService.GetTopAlbums(6, searchString: searchString);
            var albumsVM = albums.Select(x => _mapper.Map<AlbumVM>(x)).ToList();

            var model = new BrowseAllVM()
            {
                Albums = albumsVM,
                Artists = artistsVM,
                //Playlists = playlistVM,
                Tracks = tracksVM,
            };

            return View(model);
        }

        [HttpGet("{searchString}/tracks")]
        public IActionResult BrowseTracks(string searchString)
        {
            var tracks = _trackService.GetAllTracks(searchString: searchString);
            var tracksVM = tracks.Select(x => _mapper.Map<TrackVM>(x)).ToList();

            return View(tracksVM);
        }

        [HttpGet("{searchString}/albums")]
        public IActionResult BrowseAlbums(string searchString)
        {
            var albums = _albumService.GetAllAlbumsAsync(searchString: searchString);
            var albumsVM = albums.Select(x => _mapper.Map<AlbumVM>(x)).ToList();

            return View(albumsVM);
        }

        [HttpGet("{searchString}/artists")]
        public IActionResult BrowseArtists(string searchString)
        {
            var artists = _artistService.GetAllArtists(searchString: searchString);
            var artistsVM = artists.Select(x => _mapper.Map<ArtistVM>(x)).ToList();

            return View(artistsVM);
        }
    }
}
