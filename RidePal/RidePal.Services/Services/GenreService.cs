using AutoMapper;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RidePal.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GenreService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public GenreDTO GetGenreByIdAsync(Guid genreId)
        {
            var genre = _appDbContext.Genres.Where(g => g.Id == genreId).FirstOrDefault(g => g.IsDeleted == false);

            var genreDTO = _mapper.Map<GenreDTO>(genre);

            return genreDTO;
        }

        public ICollection<GenreDTO> GetAllGenresAsync()
        {
            var genres = _appDbContext.Genres.Where(g => g.IsDeleted == false);

            var genresDTO = genres.Select(g => _mapper.Map<GenreDTO>(g)).ToList();

            return genresDTO;
        }
    }
}
