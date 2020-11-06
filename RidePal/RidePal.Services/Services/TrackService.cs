using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services
{
    public class TrackService : ITrackService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TrackService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrackDTO>> GetAllTracksAsync(int? page = 1, int? pagesize = 10)
        {
            var tracks = await _appDbContext.Tracks
                .Include(track => track.TrackPlaylists)
                .Where(track => track.IsDeleted == false)
                .Select(track => _mapper.Map<TrackDTO>(track))
                .ToListAsync();

            return tracks;

        }

        public async Task<TrackDTO> GetTrackAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var track = await _appDbContext.Tracks
                .Include(x => x.TrackPlaylists)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (track == null)
                throw new ArgumentNullException();

            var artistDTO = _mapper.Map<TrackDTO>(track);

            return artistDTO;
        }
    }
}
