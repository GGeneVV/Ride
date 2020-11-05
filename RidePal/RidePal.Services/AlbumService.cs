using AutoMapper;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
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

        public ICollection<AlbumDTO> GetAllAlbumsAsync()
        {
            var albums = _appDbContext.Albums.Where(a => a.IsDeleted == false);

            var albumsDTO = albums.Select(a => _mapper.Map<AlbumDTO>(a)).ToList();

            return albumsDTO;
        }
    }
}
