﻿
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RidePal.Services.Configurations;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Web.Models.EditVM;
using System;
using System.Threading.Tasks;

namespace RidePal.Web
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistsController(IPlaylistService playlistService, IMapper mapper)
        {
            _playlistService = playlistService;
            _mapper = mapper;
        }

        // GET: api/Playlists
        [HttpGet]
        public IActionResult GetAllPlaylists()
        {
            var playlists = _playlistService.GetAllPlaylists();
            return Ok(playlists);
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDTO>> GetPlaylist(Guid? id)
        {
            PlaylistDTO playlist;
            try
            {
                playlist = await _playlistService.GetPlaylist(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok(playlist);
        }

        // PUT: api/Playlists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(Guid? id, EditPlaylistVM updatedPlaylist)
        {
            PlaylistDTO playlist;
            try
            {
                if (updatedPlaylist == null) { throw new ArgumentNullException(); }

                var updatedDTO = _mapper.Map<PlaylistDTO>(updatedPlaylist);
                playlist = await _playlistService.EditPlaylist(id, updatedDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Playlists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlaylistDTO>> PostPlaylist(int travelDuration, PlaylistConfig playlistConfig)
        {
            PlaylistDTO playlistDTO;
            try
            {
                playlistDTO = await _playlistService.GeneratePlaylist(travelDuration, playlistConfig);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Created("PostPlaylist", playlistDTO);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlaylist(Guid? id)
        {
            try
            {
                await _playlistService.DeletePlaylist(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
