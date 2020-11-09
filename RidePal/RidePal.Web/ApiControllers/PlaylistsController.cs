using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Models;

namespace RidePal.Web
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlaylistsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _context.Playlists.ToListAsync();
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(Guid? id)
        {
            var playlist = await _context.Playlists.FindAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        // PUT: api/Playlists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(Guid? id, Playlist playlist)
        {
            if (id != playlist.UserId)
            {
                return BadRequest();
            }

            _context.Entry(playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Playlists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistExists(playlist.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylist", new { id = playlist.UserId }, playlist);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Playlist>> DeletePlaylist(Guid? id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return playlist;
        }

        private bool PlaylistExists(Guid? id)
        {
            return _context.Playlists.Any(e => e.UserId == id);
        }
    }
}
