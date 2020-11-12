using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IGenreService
    {
        GenreDTO GetGenreByIdAsync(Guid genreId);

        PaginatedList<GenreDTO> GetAllGenresAsync(int? pageNumber=1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
    }
}
