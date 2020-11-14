﻿using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IArtistService
    {
        Task<ArtistDTO> GetArtistAsync(Guid id);
        PaginatedList<ArtistDTO> GetAllArtists(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");

        Task<IReadOnlyCollection<ArtistDTO>> GetTopArtistsAsync(int count = 5);
    }
}
