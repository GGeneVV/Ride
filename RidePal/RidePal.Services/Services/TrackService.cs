using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.Extensions;
using RidePal.Services.Pagination;
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

        public PaginatedList<TrackDTO> GetAllTracks(
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
            int pageSize = 10;

            currentFilter = searchString;

            var tracks = _appDbContext.Tracks

                .Include(t => t.Album)
                .Include(t => t.Artist)
                .Include(t => t.Genre)
                .Include(t => t.TrackPlaylists)
                .Where(t => t.IsDeleted == false)
                .WhereIf(!String.IsNullOrEmpty(searchString), s => s.Title.Contains(searchString))
                .Select(t => _mapper.Map<TrackDTO>(t)).Take(pageSize);
            

            switch (sortOrder.ToLower())
            {
                case "rank_desc":
                    tracks = tracks.OrderByDescending(b => b.Rank);
                    break;
                case "title":
                    tracks = tracks.OrderBy(b => b.Title);
                    break;
                case "title_decs":
                    tracks = tracks.OrderByDescending(s => s.Title);
                    break;
                case "artist":
                    tracks = tracks.OrderBy(b => b.Artist);
                    break;
                case "artist_decs":
                    tracks = tracks.OrderByDescending(s => s.Artist);
                    break;
                case "duration":
                    tracks = tracks.OrderBy(b => b.Duration);
                    break;
                case "duration_decs":
                    tracks = tracks.OrderByDescending(s => s.Duration);
                    break;
                default:
                    tracks = tracks.OrderBy(s => s.Rank);
                    break;
            }

            


            return PaginatedList<TrackDTO>.Create(tracks.AsQueryable(), pageNumber ?? 1, pageSize);

        }

        public async Task<TrackDTO> GetTrackAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var track = await _appDbContext.Tracks
                .Include(t => t.Album)
                .Include(t => t.Artist)
                .Include(t => t.Genre)
                .Include(t => t.TrackPlaylists)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (track == null)
                throw new ArgumentNullException();

            var trackDTO = _mapper.Map<TrackDTO>(track);

            return trackDTO;
        }

        public async Task<IReadOnlyCollection<TrackDTO>> GetPopularTracksAsync(int count = 5)
        {
            var tracks = await _appDbContext.Tracks
                .Include(t => t.Album)
                .Include(t => t.Artist)
                .Include(t => t.Genre)
                .Include(t => t.TrackPlaylists)
                .Where(t => t.IsDeleted == false)
                .OrderByDescending(t => t.ReleaseDate)
                .Take(count)
                .Select(t => _mapper.Map<TrackDTO>(t))
                .ToListAsync();

            return tracks;
        }

        public IReadOnlyCollection<TrackDTO> GetTopTracks(int count = 5, string searchString = "")
        {
            IReadOnlyCollection<TrackDTO> tracks = new List<TrackDTO>();
            var query = _appDbContext.Tracks
                .Include(t => t.Album)
                .Include(t => t.Artist)
                .Include(t => t.Genre)
                .Include(t => t.TrackPlaylists)
                .Where(t => t.IsDeleted == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));
            }
            tracks = query
                .OrderByDescending(x => x.Rank)
                .Take(count)
                .Select(a => _mapper.Map<TrackDTO>(a))
                .ToList();

            return tracks;
        }
    }
}
