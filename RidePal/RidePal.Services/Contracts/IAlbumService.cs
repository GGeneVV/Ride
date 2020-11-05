using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;

namespace RidePal.Services.Contracts
{
    public interface IAlbumService
    {
        AlbumDTO GetAlbumByIdAsync(Guid albumId);

        ICollection<AlbumDTO> GetAllAlbumsAsync();

    }
}
