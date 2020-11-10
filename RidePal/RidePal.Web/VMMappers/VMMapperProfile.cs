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

            CreateMap<TrackDTO, TrackVM>().ReverseMap();

            CreateMap<TrackPlaylistDTO, TrackPlaylistVM>().ReverseMap();

            CreateMap<AlbumDTO, AlbumVM>().ReverseMap();

            CreateMap<ArtistDTO, ArtistVM>().ReverseMap();

            CreateMap<GenreDTO, GenreVM>().ReverseMap();

            CreateMap<TrackDTO, TrackVM>().ForMember(g => g.Genre, opt => opt.MapFrom(s => s.Genre)).ReverseMap();

            CreateMap<UserDTO, UserVM>().ReverseMap();
        }
    }
}
