using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Services.Pagination;
using RidePal.Web.Models;
using RidePal.Web.Models.EditVM;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Web.Controllers
{
    public class PlaylistsController : Controller
    {

        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PlaylistsController(IPlaylistService playlistService, IMapper mapper, IUserService userService)
        {

            _playlistService = playlistService;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: Playlists
        public IActionResult Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            int pageSize = 5;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "rank_desc" : "";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "duration_desc" : "Duration";

            var playlists = _playlistService
                 .GetAllPlaylists(pageNumber, sortOrder, currentFilter, searchString)
                 .Select(p => _mapper.Map<PlaylistVM>(p));

            return View(PaginatedList<PlaylistVM>.Create(playlists.AsQueryable(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult MyPlaylists(Guid UserId,
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            var playlistst = _playlistService.GetUserPlaylists(UserId, pageNumber, sortOrder, currentFilter, searchString)
                .Select(p => _mapper.Map<PlaylistVM>(p));

            int pageSize = 5;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "tracks_desc" : "";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "duration_desc" : "Duration";

            return View(PaginatedList<PlaylistVM>.Create(playlistst.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Playlists/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistVM = _mapper.Map<PlaylistVM>(_playlistService.GetPlaylist(id));

            if (playlistVM == null)
            {
                return NotFound();
            }

            return View(playlistVM);
        }

        // GET: Playlists/Create


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePlaylist(int travelDuration, PlaylistConfigVM playlistConfig)
        {
            var dto = _mapper.Map<PlaylistConfigDTO>(playlistConfig);
            var playlistDTO = await _playlistService.GeneratePlaylist(travelDuration, dto);
            var playlistVM = _mapper.Map<PlaylistVM>(playlistDTO);

            return PartialView("_PlaylistPartial", playlistVM);
        }

        // GET: Playlists/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = _playlistService.GetPlaylist(id);

            if (playlist == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_userService.GetAllUsersAsync(), "Id", "Id", playlist.UserId);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id,
            [Bind("Id,Title")] EditPlaylistVM newPlaylist,
            [Bind("Id,UserId")] PlaylistVM playlist)
        {

            try
            {
                await _playlistService.EditPlaylist(id, _mapper.Map<PlaylistDTO>(newPlaylist));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(playlist.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = _playlistService.GetPlaylist(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid? id)
        {
            var playlist = _playlistService.DeletePlaylist(id);

            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(Guid? id)
        {
            return _playlistService.PlaylistExists(id);
        }
    }
}
