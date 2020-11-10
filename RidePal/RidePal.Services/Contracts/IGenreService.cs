using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IGenreService
    {
        GenreDTO GetGenreByIdAsync(Guid genreId);

        Task<IReadOnlyCollection<GenreDTO>> GetAllGenresAsync();
    }
}
