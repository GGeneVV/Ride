using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services
{
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ArtistService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IQueryable<ArtistDTO> GetAllArtists(
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

            var artists = _appDbContext.Artists
                .Include(artist => artist.Albums)
                .Include(artist => artist.Tracks)
                .AsNoTracking()
                .Where(g => g.IsDeleted == false)
                .WhereIf(!String.IsNullOrEmpty(searchString), s => s.Name.Contains(searchString))
                .Select(g => _mapper.Map<ArtistDTO>(g));



            switch (sortOrder)
            {
                case "ArtistsByAlbumsCount_desc":
                    artists = artists.OrderByDescending(b => b.Albums.Count);
                    break;
                case "Name":
                    artists = artists.OrderBy(b => b.Name);
                    break;
                case "Name_desc":
                    artists = artists.OrderByDescending(s => s.Name);
                    break;
                default:
                    artists = artists.OrderBy(s => s.Albums.Count);
                    break;
            }


            return artists.AsQueryable();
        }

        public async Task<ArtistDTO> GetArtistAsync(Guid id)
        {
            if (id == null)

            {
                return null;
            }

            var artist = await _appDbContext.Artists
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .Include(x => x.Albums)
                .Include(x => x.Tracks)
                .FirstOrDefaultAsync();

            if (artist == null)
            {
                return null;
            }

            var artistDTO = _mapper.Map<ArtistDTO>(artist);

            return artistDTO;
        }

        public IReadOnlyCollection<ArtistDTO> GetTopArtists(int count = 5, string searchString = "")
        {
            IReadOnlyCollection<ArtistDTO> artists = new List<ArtistDTO>();
            var query = _appDbContext.Artists
                .AsNoTracking()
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
            }
            artists = query
                .OrderByDescending(x => x.Tracks.Count)
                .Take(count)
                .Include(x => x.Albums)
                .Include(x => x.Tracks)
                .Select(a => _mapper.Map<ArtistDTO>(a))
                .ToList();


            return artists;
        }
    }
}
