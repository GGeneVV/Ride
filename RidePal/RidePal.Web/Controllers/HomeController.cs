using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RidePal.Services.Configurations;
using RidePal.Services.Contracts;
using RidePal.Web.Models;
using System.Collections.Generic;
using System.Diagnostics;
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
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            //Test purpose
            var playlistConfig = new PlaylistConfig()
            {
                UseTopTracks = true,
                IsAdvanced = true,
                GenreConfigs = new List<PlaylistGenreConfig>()
                {
                    new PlaylistGenreConfig()
                    {
                        IsChecked = true,
                        Name = "Pop",
                        Percentage = 80

                    },
                    new PlaylistGenreConfig()
                    {
                        IsChecked = true,
                        Name = "Rock",
                        Percentage = 20
                    },
                }
            };

            var model = new HomeIndexVM()
            {
                PlaylistConfig = playlistConfig
            };
            //var playlist = await _playlistService.GeneratePlaylist(2850, playlistConfig);
            //var playlistVM = _mapper.Map<PlaylistVM>(playlist);
            //Test purpose--------------
            //int pop = playlist.TrackPlaylists.Select(x => x.Track.Genre).Where(x => x.Name == "Pop").Count();
            //int rock = playlist.TrackPlaylists.Select(x => x.Track.Genre).Where(x => x.Name == "Rock").Count();
            //--------------------------
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
