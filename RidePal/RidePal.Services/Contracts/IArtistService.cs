using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IArtistService
    {
        Task<ArtistDTO> GetArtistAsync(Guid id);
        Task<IEnumerable<ArtistDTO>> GetAllArtistsAsync(int? page = 1, int? pagesize = 10);
    }
}
