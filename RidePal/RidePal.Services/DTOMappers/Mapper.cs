using AutoMapper;
using RidePal.Models;
using RidePal.Services.DTOModels;

namespace RidePal.Services.DTOMappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Album, AlbumDTO>().ReverseMap();

            CreateMap<Artist, ArtistDTO>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();

            CreateMap<Playlist, PlaylistDTO>().ReverseMap();

            CreateMap<Track, TrackDTO>().ForMember(g => g.Genre, opt => opt.MapFrom(s => s.Genre)).ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }

}