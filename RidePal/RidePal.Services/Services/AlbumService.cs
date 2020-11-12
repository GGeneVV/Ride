using AutoMapper;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.Extensions;
using RidePal.Services.Pagination;
using System;
using System.Linq;

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

        public AlbumDTO GetAlbumByIdAsync(Guid albumId)
        {
            var album = _appDbContext.Albums.Where(a => a.Id == albumId).FirstOrDefault(a => a.IsDeleted == false);

            var albumDTO = _mapper.Map<AlbumDTO>(album);

            return albumDTO;
        }

        public PaginatedList<AlbumDTO> GetAllAlbumsAsync(
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
                case "NameOfArtist_decs":
                    albums = albums.OrderByDescending(a => a.Artist.Name);
                    break;
                default:
                    albums = albums.OrderBy(a => a.Title);
                    break;
            }

            int pageSize = 10;

            return PaginatedList<AlbumDTO>.Create(albums.AsQueryable(), pageNumber ?? 1, pageSize);

        }
    }
}
