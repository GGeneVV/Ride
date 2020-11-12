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
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IGenreService genreService, IPlaylistService playlistService, IMapper mapper)
        {
            _logger = logger;
            _genreService = genreService;
            _playlistService = playlistService;
            _mapper = mapper;

        }


        public async Task<IActionResult> Index([Bind("Title,UseTopTracks,AllowTracksFromSameArtist,IsAdvanced")] PlaylistConfigVM playlistConfigVM,
            [Bind("IsChecked,Percentage")] GenreConfigVM genreVM)
        {

            var genres = _genreService.GetAllGenresAsync();
            var genreConfigs = genres.Select(g => _mapper.Map<GenreConfigVM>(g)).ToList();


            var playlistConfig = new PlaylistConfigVM()
            {
                Title = playlistConfigVM.Title,
                UseTopTracks = playlistConfigVM.UseTopTracks,
                IsAdvanced = playlistConfigVM.IsAdvanced,
                GenreConfigs = genreConfigs
            };

            var model = new HomeIndexVM()
            {
                PlaylistConfig = playlistConfig
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
