using AutoMapper;
using RidePal.Services.DTOModels;
using RidePal.Web.Models;
using RidePal.Web.Models.EditVM;
using System.Linq;

namespace RidePal.Web.VMMappers
{
    public class VMMapperProfile : Profile
    {
        public VMMapperProfile()
        {
            CreateMap<EditPlaylistVM, PlaylistDTO>().ReverseMap();
            CreateMap<PlaylistDTO, PlaylistVM>().ReverseMap();

        }
    }
}
