using AutoMapper;
using Newtonsoft.Json;
using RidePal.Data;
using RidePal.Models.DataSource;
using RidePal.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task GetGenresAsync()
        {
            var client = new HttpClient();

            using (var response = await client.GetAsync("https://api.deezer.com/genre"))
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<DataGenre>(responseAsString);

                for (int i = 1; i < 6; i++)
                {
                    await _appDbContext.Genres.AddAsync(result.Genres[i]);
                }

                await _appDbContext.SaveChangesAsync();
            }
        }

    }
}
