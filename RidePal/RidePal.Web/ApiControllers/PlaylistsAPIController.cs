using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Web.Models.EditVM;
using System;
using System.Threading.Tasks;
using RidePal.Web.Models;

namespace RidePal.Web
{

    [Route("api/Playlists")]
    [ApiController]
    public class PlaylistsAPIController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistsAPIController(IPlaylistService playlistService, IMapper mapper)
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
        public async Task<ActionResult<PlaylistDTO>> GetPlaylist(Guid id)
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
        public async Task<IActionResult> PutPlaylist([Bind("Id,UserId,Revive")] EditPlaylistVM editPlaylistVM)
        {
            PlaylistDTO playlist;
            try
            {
                if (string.IsNullOrEmpty(editPlaylistVM.Title)) { throw new ArgumentNullException(); }

                var editPlaylistDTO = _mapper.Map<EditPlaylistDTO>(editPlaylistVM);
                playlist = await _playlistService.EditPlaylist(editPlaylistDTO);
                return Ok(playlist);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        // POST: api/Playlists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlaylistDTO>> PostPlaylist(
            [Bind("TravelDuration,Title,UseTopTracks,AllowTracksFromSameArtist,IsAdvanced,IsArtistRepeated," +
            "IsTopTracksEnabled,UserId,GenreConfigs,Name,IsChecked,Percentage")
            ]GeneratePlaylistAPIVM generatePlaylistAPIVM)
        {
            PlaylistDTO playlistDTO;
            try
            {
                var config = _mapper.Map<PlaylistConfigDTO>(generatePlaylistAPIVM);
                playlistDTO = await _playlistService
                    .GeneratePlaylist(generatePlaylistAPIVM.TravelDuration, config, generatePlaylistAPIVM.userId);

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(playlistDTO);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlaylist(Guid id)
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
