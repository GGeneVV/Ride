using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RidePal.Models;
using RidePal.Models.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RidePal.Data.Seeder
{
    public static class Seeder
    {
        public static async Task SeedDbAsync(this AppDbContext _appDbContext)
        {
            var client = new HttpClient();

            using (var response = await client.GetAsync("https://api.deezer.com/genre"))
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<DataGenre>(responseAsString);

                for (int i = 1; i < 6; i++) //result.Genres.Count()
                {
                    var genre = result.Genres[i];
                    await _appDbContext.Genres.AddAsync(genre);


                    using (var responseSecond = await client.GetAsync($"https://api.deezer.com/search/playlist?q={genre.Name}&limit=50"))
                    {
                        var responseSecondAsString = await responseSecond.Content.ReadAsStringAsync();

                        var playlistTracks = JObject.Parse(responseSecondAsString)["data"];

                        for (int t = 0; t < playlistTracks.Count(); t++)
                        {
                            var tracklistURL = playlistTracks[t]["tracklist"].ToString();

                            using (var responseThird = await client.GetAsync($"{tracklistURL}&limit=100"))
                            {
                                var responseThirdAsString = await responseThird.Content.ReadAsStringAsync();

                                var tracks = JObject.Parse(responseThirdAsString)["data"]; //tracks as json


                                for (int j = 0; j < tracks.Count(); j++)
                                {
                                    var trackAsObject = JsonConvert.DeserializeObject<Track>(tracks[j].ToString());
                                    var trackArtist = JsonConvert.DeserializeObject<Artist>(tracks[j]["artist"].ToString());
                                    var trackAlbum = JsonConvert.DeserializeObject<Album>(tracks[j]["album"].ToString());

                                    await _appDbContext.Artists.AddAsync(trackArtist);
                                    trackAsObject.Artist = trackArtist;

                                    trackAlbum.Artist = trackArtist;
                                    await _appDbContext.Albums.AddAsync(trackAlbum);
                                    trackAsObject.Album = trackAlbum;

                                    trackAsObject.Genre = genre;
                                    await _appDbContext.Tracks.AddAsync(trackAsObject);
                                    await _appDbContext.SaveChangesAsync();
                                }
                            }

                        }

                    }
                }

                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
