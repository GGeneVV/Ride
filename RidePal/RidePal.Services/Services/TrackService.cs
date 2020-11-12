using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.Extensions;
using RidePal.Services.Pagination;
using System;
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

        public PaginatedList<TrackDTO> GetAllTracksAsync(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            currentFilter = searchString;

            var tracks = _appDbContext.Tracks
                .Include(t => t.TrackPlaylists)
                .Where(t => t.IsDeleted == false)
                .WhereIf(!String.IsNullOrEmpty(searchString), s => s.Title.Contains(searchString))
                .Select(t => _mapper.Map<TrackDTO>(t));

            //t => _mapper.Map<TrackDTO>(t, t => _mapper.Map<TrackPlaylistDTO>(t))

            switch (sortOrder)
            {
                case "tracks_desc":
                    tracks = tracks.OrderByDescending(b => b.Rank);
                    break;
                case "Title":
                    tracks = tracks.OrderBy(b => b.Title);
                    break;
                case "Title_decs":
                    tracks = tracks.OrderByDescending(s => s.Title);
                    break;
                case "Artist":
                    tracks = tracks.OrderBy(b => b.Artist);
                    break;
                case "Artist_decs":
                    tracks = tracks.OrderByDescending(s => s.Artist);
                    break;
                case "Duration":
                    tracks = tracks.OrderBy(b => b.Duration);
                    break;
                case "Duration_decs":
                    tracks = tracks.OrderByDescending(s => s.Duration);
                    break;
                default:
                    tracks = tracks.OrderBy(s => s.Rank);
                    break;
            }

            int pageSize = 10;


            return PaginatedList<TrackDTO>.Create(tracks.AsQueryable(), pageNumber ?? 1, pageSize);

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

            var trackDTO = _mapper.Map<TrackDTO>(track);

            return trackDTO;
        }
    }
}
