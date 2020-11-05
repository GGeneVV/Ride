﻿using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;

namespace RidePal.Services.Contracts
{
    public interface IGenreService
    {
        GenreDTO GetGenreByIdAsync(Guid genreId);

        ICollection<GenreDTO> GetAllGenresAsync();
    }
}
