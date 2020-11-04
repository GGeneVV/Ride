using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RidePal.Services;
using RidePal.Services.Contracts;
using RidePal.Web.Models;

namespace RidePal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenreService _genreService;

        public HomeController(ILogger<HomeController> logger, IGenreService genreService)
        {
            _logger = logger;
            _genreService = genreService;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            //await _genreService.GetGenresAsync();
            //await _genreService.GetAlbumsAsync();
            await _genreService.GetTracksAsync();
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
