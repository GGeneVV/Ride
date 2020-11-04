﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IGenreService
    {
        Task GetGenresAsync();

        Task GetAlbumsAsync();

        Task GetTracksAsync();
    }
}