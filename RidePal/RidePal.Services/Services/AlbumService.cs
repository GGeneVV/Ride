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
    public class AlbumService : IAlbumService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AlbumService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<AlbumDTO> GetAlbumByIdAsync(Guid albumId)
        {
            if (albumId == null)

            {
                return null;
            }

            var album = await _appDbContext.Albums
                .AsNoTracking()
                .Where(a => a.Id == albumId)
                .FirstOrDefaultAsync(a => a.IsDeleted == false);
            if (album == null)
            {
                return null;
            }
            var albumDTO = _mapper.Map<AlbumDTO>(album);

            return albumDTO;
        }

        public IQueryable<AlbumDTO> GetAllAlbumsAsync(
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

            var albums = _appDbContext.Albums
                .AsNoTracking()
                .Where(a => a.IsDeleted == false)
                .WhereIf(!String.IsNullOrEmpty(searchString), a => a.Title.Contains(searchString))
                .Select(a => _mapper.Map<AlbumDTO>(a));

            switch (sortOrder)
            {
                case "title_desc":
                    albums = albums.OrderByDescending(a => a.Title);
                    break;
                case "NameOfArtist":
                    albums = albums.OrderBy(a => a.Artist.Name);
                    break;
                case "NameOfArtist_desc":
                    albums = albums.OrderByDescending(a => a.Artist.Name);
                    break;
                default:
                    albums = albums.OrderBy(a => a.Title);
                    break;
            }

            return albums.AsQueryable();
        }

        public IReadOnlyCollection<AlbumDTO> GetTopAlbums(int count = 5, string searchString = "")
        {
            IReadOnlyCollection<AlbumDTO> albums = new List<AlbumDTO>();
            var query = _appDbContext.Albums
                .Include(x => x.Artist)
                .Include(x => x.Tracks)
                .AsNoTracking()
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));
            }
            albums = query
                .OrderByDescending(x => x.Tracks.Count)
                .Take(count)
                .Select(a => _mapper.Map<AlbumDTO>(a))
                .ToList();

            return albums;
        }
    }
}
