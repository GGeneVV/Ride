using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RidePal.Services.Contracts;
using RidePal.Web.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenreService _genreService;
        private readonly IPlaylistService _playlistService;
        private readonly ITrackService _trackService;
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IGenreService genreService, IPlaylistService playlistService, ITrackService trackService, IArtistService artistService, IMapper mapper)
        {
            _logger = logger;
            _genreService = genreService;
            _playlistService = playlistService;
            _trackService = trackService;
            _artistService = artistService;
            _mapper = mapper;

        }


        public async Task<IActionResult> Index([Bind("Title,UseTopTracks,AllowTracksFromSameArtist,IsAdvanced")] PlaylistConfigVM playlistConfigVM,
            [Bind("IsChecked,Percentage")] GenreConfigVM genreVM)
        {

            var genres = _genreService.GetAllGenresAsync();
            var genreConfigs = genres.Select(g => _mapper.Map<GenreConfigVM>(g)).ToList();
            var popularTracks = await _trackService.GetPopularTracksAsync(5);
            var popularTracksVM = popularTracks.Select(t => _mapper.Map<TrackVM>(t)).ToList();

            var topTracks = await _trackService.GetTopTracksAsync(5);
            var topTracksVM = topTracks.Select(t => _mapper.Map<TrackVM>(t)).ToList();

            var topArists = await _artistService.GetTopArtistsAsync(5);
            var topArtistsVM = topArists.Select(t => _mapper.Map<ArtistVM>(t)).ToList();

            var playlistConfig = new PlaylistConfigVM()
            {
                Title = playlistConfigVM.Title,
                UseTopTracks = playlistConfigVM.UseTopTracks,
                IsAdvanced = playlistConfigVM.IsAdvanced,
                GenreConfigs = genreConfigs
            };

            var model = new HomeIndexVM()
            {
                PlaylistConfig = playlistConfig,
                PopularTracks = popularTracksVM,
                TopTracks = topTracksVM,
                TopArtists = topArtistsVM,

            };


            return View(model);
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
