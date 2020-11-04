using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RidePal.Data;
using RidePal.Models;
using RidePal.Models.DataSource;
using RidePal.Services.Contracts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RidePal.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GenreService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }



    }
}
