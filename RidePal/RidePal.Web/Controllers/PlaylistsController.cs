using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Services.Contracts;
using RidePal.Web.Models;

namespace RidePal.Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistsController(AppDbContext context, IPlaylistService playlistService, IMapper mapper)
        {
            _context = context;
            _playlistService = playlistService;
            _mapper = mapper;
        }

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Playlists.Include(p => p.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Picture,Duration,Rank,Enabled,IsArtistRepeated,IsTopTracksEnabled,UserId,CreatedOn,ModifiedOn,IsDeleted")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId);
            return View(playlist);
        }

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
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,Title,Picture,Duration,Rank,Enabled,IsArtistRepeated,IsTopTracksEnabled,UserId,CreatedOn,ModifiedOn,IsDeleted")] Playlist playlist)
        {
            if (id != playlist.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId);
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(Guid? id)
        {
            return _context.Playlists.Any(e => e.UserId == id);
        }
    }
}
