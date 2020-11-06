using AutoMapper;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Services.Services
{
    public class TrackPlaylistService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public TrackPlaylistService(AppDbContext appDbContext , IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

        }
        public TrackPlaylistDTO GetTrackPlaylistByIdAsync(Guid trackPlaylistId)
        {
            
        }
    }
}
