using AutoMapper;
using RidePal.Services.DTOModels;
using RidePal.Web.Models;
using RidePal.Web.Models.EditVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Web.VMMappers
{
    public class VMMapperProfile : Profile
    {
        public VMMapperProfile()
        {
            CreateMap<EditPlaylistVM, PlaylistDTO>().ReverseMap();
            CreateMap<PlaylistVM, PlaylistDTO>().ReverseMap();

        }
    }
}
