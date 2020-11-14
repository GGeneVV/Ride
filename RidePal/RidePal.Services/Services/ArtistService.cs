﻿using AutoMapper;
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
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ArtistService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public PaginatedList<ArtistDTO> GetAllArtists(
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
                case "Name_decs":
                    artists = artists.OrderByDescending(s => s.Name);
                    break;
                default:
                    artists = artists.OrderBy(s => s.Albums.Count);
                    break;
            }

            int pageSize = 10;

            return PaginatedList<ArtistDTO>.Create(artists.AsQueryable(), pageNumber ?? 1, pageSize);

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

        public async Task<IReadOnlyCollection<ArtistDTO>> GetTopArtistsAsync(int count = 5)
        {
            var artists = await _appDbContext.Artists
                .Include(x => x.Albums)
                .Include(x => x.Tracks)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Tracks.Count)
                .Take(count)
                .Select(a => _mapper.Map<ArtistDTO>(a))
                .ToListAsync();

            return artists;
        }
    }
}
