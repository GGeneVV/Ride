using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RidePal.Data;
using RidePal.Models;
using RidePal.Models.DataSource;
using RidePal.Services.Contracts;
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

        public async Task GetAlbumsAsync()
        {
            var client = new HttpClient();

            using (var response = await client.GetAsync("https://api.deezer.com/album/302127"))
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var app = JObject.Parse(responseAsString)["genres"]["data"][0]["name"].ToString();//["data"]["name"];
                //var AppId = JObject.Parse(result)["log"].SelectToken("$.Response[?(@.@type=='GetApplication')]")["AppId"].ToString();

                //for (int i = 1; i < 6; i++)
                //{
                //    await _appDbContext.Genres.AddAsync(result.Genres[i]);
                //}

                //await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task GetTracksAsync()
        {
            var client = new HttpClient();

            using (var response = await client.GetAsync("https://api.deezer.com/playlist/908622995"))
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                //var app = JObject.Parse(responseAsString)["genres"]["data"][0]["name"].ToString();//["data"]["name"];
                //var AppId = JObject.Parse(result)["log"].SelectToken("$.Response[?(@.@type=='GetApplication')]")["AppId"].ToString();


                for (int i = 0; i < 50; i++)
                {
                    var track = JObject.Parse(responseAsString)["tracks"]["data"][i]; //tracks as json

                    var trackAsObject = JsonConvert.DeserializeObject<Track>(track.ToString());
                    var trackArtist = JsonConvert.DeserializeObject<Artist>(track["artist"].ToString());
                    var trackAlbum = JsonConvert.DeserializeObject<Album>(track["album"].ToString());

                    await _appDbContext.Artists.AddAsync(trackArtist);
                    //await _appDbContext.SaveChangesAsync();
                    trackAsObject.Artist = trackArtist;
                    //trackAsObject.ArtistId = trackArtist.Id;

                    trackAlbum.Artist = trackArtist;
                    await _appDbContext.Albums.AddAsync(trackAlbum);
                    //await _appDbContext.SaveChangesAsync();
                    trackAsObject.Album = trackAlbum;
                    //trackAsObject.AlbumId = trackAlbum.Id;

                    await _appDbContext.Tracks.AddAsync(trackAsObject);
                    await _appDbContext.SaveChangesAsync();
                }
            }
        }

    }
}
