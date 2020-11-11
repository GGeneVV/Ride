using AutoMapper;
using RidePal.Data.Configurations;
using RidePal.Services.DTOModels.Configurations;
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

            CreateMap<PlaylistConfigVM, PlaylistConfigDTO>()
                .ForMember(g=> g.AllowTracksFromSameArtist, opt => opt.MapFrom(s => s.AllowTracksFromSameArtist))
                .ForMember(g => g.IsAdvanced, opt => opt.MapFrom(s => s.IsAdvanced))
                .ForMember(g => g.UseTopTracks, opt => opt.MapFrom(s => s.UseTopTracks))
                .ForMember(g => g.GenreConfigs, opt => opt.MapFrom(s => s.GenreConfigs))
                .ReverseMap();

            CreateMap<TrackDTO, TrackVM>().ReverseMap();

            CreateMap<TrackPlaylistDTO, TrackPlaylistVM>().ReverseMap();

            CreateMap<AlbumDTO, AlbumVM>().ReverseMap();

            CreateMap<ArtistDTO, ArtistVM>().ReverseMap();
            CreateMap<PlaylistGenreConfigDTO, GenreVM>().ReverseMap();


            CreateMap<GenreDTO, GenreVM>().ReverseMap();

            CreateMap<TrackDTO, TrackVM>().ForMember(g => g.Genre, opt => opt.MapFrom(s => s.Genre)).ReverseMap();

            CreateMap<UserDTO, UserVM>().ReverseMap();
        }
    }
}
