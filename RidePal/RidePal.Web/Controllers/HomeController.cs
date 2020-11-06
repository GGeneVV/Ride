using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RidePal.Services.Contracts;
using RidePal.Web.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RidePal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenreService _genreService;
        private readonly IPlaylistService _playlistService;

        public HomeController(ILogger<HomeController> logger, IGenreService genreService, IPlaylistService playlistService)
        {
            _logger = logger;
            _genreService = genreService;
            _playlistService = playlistService;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var playlist = await _playlistService.GeneratePlaylist(7850, pop: true);
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
