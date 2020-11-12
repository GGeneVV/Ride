using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Logging;
using RidePal.Data;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Services.Contracts;
using RidePal.Services.Extensions;
using RidePal.Web.Models;
using System.Collections.Generic;
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
        
        
        public async Task<IActionResult> Index([Bind("UseTopTracks,AllowTracksFromSameArtist,IsAdvanced")] PlaylistConfigVM playlistConfigVM,
            [Bind("IsChecked,Percentage")] GenreConfigVM genreVM)
        {

            var genres = _genreService.GetAllGenresAsync();
            var genreConfigs = genres.Select(g=>_mapper.Map<GenreConfigVM>(g)).ToList();
            
            //Test purpose
            var playlistConfig = new PlaylistConfigVM()
            {
                UseTopTracks = playlistConfigVM.UseTopTracks,
                IsAdvanced = playlistConfigVM.IsAdvanced,
                GenreConfigs = genreConfigs
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
