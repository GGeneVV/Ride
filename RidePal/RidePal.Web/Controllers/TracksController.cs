using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RidePal.Services.Contracts;
using RidePal.Services.Pagination;
using RidePal.Web.Models;
using System;
using System.Linq;

namespace RidePal.Web.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public TracksController(ITrackService trackService, IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        // GET: Tracks
        public IActionResult Index(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["RankSortParm"] = String.IsNullOrEmpty(sortOrder) ? "rank_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["ArtistSortParm"] = sortOrder == "Artist" ? "Artist_desc" : "Artist";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "duration_desc" : "Duration";

            int pageSize = 10;

            var tracks = _trackService.GetAllTracks(pageNumber, sortOrder, currentFilter, searchString);
            var tracksVM = tracks.Select(t => _mapper.Map<TrackVM>(t));

            var tracksPaginated = PaginatedList<TrackVM>.Create(tracksVM.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(tracksPaginated);
        }

        // GET: Tracks/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackVM = _mapper.Map<TrackVM>(_trackService.GetTrackAsync(id));

            if (trackVM == null)
            {
                return NotFound();
            }

            return View(trackVM);
        }
    }
}
