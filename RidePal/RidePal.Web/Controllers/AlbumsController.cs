using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RidePal.Services.Contracts;
using RidePal.Services.Pagination;
using RidePal.Web.Models;
using System;
using System.Linq;

namespace RidePal.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumsController(IAlbumService albumService, IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        // GET: Albums
        public IActionResult Index(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            var albumsVM = _albumService.GetAllAlbumsAsync(pageNumber, sortOrder, currentFilter, searchString)
                .Select(g => _mapper.Map<AlbumVM>(g));

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["NameOfArtistSortParm"] = sortOrder == "NameOfArtist" ? "NameOfArtist_desc" : "NameOfArtist";

            int pageSize = 10;
            return View(PaginatedList<AlbumVM>.Create(albumsVM.AsQueryable(), pageNumber ?? 1, pageSize));
        }
        // GET: Albums/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumVM = _mapper.Map<AlbumVM>(_albumService.GetAlbumByIdAsync(id));

            if (albumVM == null)
            {
                return NotFound();
            }

            return View(albumVM);
        }
    }
}
