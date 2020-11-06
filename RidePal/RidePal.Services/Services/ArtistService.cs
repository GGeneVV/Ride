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
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ArtistService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArtistDTO>> GetAllArtistsAsync(int? page = 1, int? pagesize = 10)
        {
            var artists = await _appDbContext.Artists
                .Include(artist => artist.Albums)
                .Include(artist => artist.Tracks)
                .Where(artist => artist.IsDeleted == false)
                .Select(artist => _mapper.Map<ArtistDTO>(artist))
                .ToListAsync();

            return artists;

        }

        public async Task<ArtistDTO> GetArtistAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var artist = await _appDbContext.Artists
                .Include(x => x.Albums)
                .Include(x => x.Tracks)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (artist == null)
                throw new ArgumentNullException();

            var artistDTO = _mapper.Map<ArtistDTO>(artist);

            return artistDTO;
        }
    }
}
