using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RidePal.Services.Contracts;
using RidePal.Services.Pagination;
using RidePal.Web.Models;
using System;
using System.Linq;

namespace RidePal.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        // GET: Genres
        public IActionResult Index(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            var genresVM = _genreService.GetAllGenres(pageNumber, sortOrder, currentFilter, searchString)
                .Select(g => _mapper.Map<GenreVM>(g));

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "tracks_desc" : "";
            ViewData["DurationSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";

            int pageSize = 10;
            return View(PaginatedList<GenreVM>.Create(genresVM.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Genres/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreVM = _mapper.Map<GenreVM>(_genreService.GetGenreByIdAsync(id));

            if (genreVM == null)
            {
                return NotFound();
            }

            return View(genreVM);
        }


    }
}
