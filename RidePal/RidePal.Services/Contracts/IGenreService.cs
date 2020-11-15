using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;

namespace RidePal.Services.Contracts
{
    public interface IGenreService
    {
        GenreDTO GetGenreByIdAsync(Guid genreId);

        PaginatedList<GenreDTO> GetAllGenres(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
    }
}
