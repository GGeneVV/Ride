using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RidePal.Models;
using RidePal.Models.DataSource;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RidePal.Data.Seeder
{
    public static class Seeder
    {
        public static async Task SeedDbAsync(this AppDbContext _appDbContext)
        {
            var client = new HttpClient();

            var artists = new HashSet<Artist>();
            var artistDeezerId = new HashSet<string>();
            var albums = new HashSet<Album>();
            var albumDeezerId = new HashSet<string>();

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

                        var counter = 0;

                        for (int t = 0; t < playlistTracks.Count(); t++)
                        {
                            if (counter >= 1500) { break; }

                            var tracklistURL = playlistTracks[t]["tracklist"].ToString();

                            using (var responseThird = await client.GetAsync($"{tracklistURL}&limit=100"))
                            {
                                var responseThirdAsString = await responseThird.Content.ReadAsStringAsync();

                                var tracks = JObject.Parse(responseThirdAsString)["data"]; //tracks as json


                                for (int j = 0; j < tracks.Count(); j++)
                                {
                                    counter++;
                                    if (counter >= 1500) { break; }

                                    var trackAsObject = JsonConvert.DeserializeObject<Track>(tracks[j].ToString());
                                    var trackArtist = JsonConvert.DeserializeObject<Artist>(tracks[j]["artist"].ToString());
                                    var trackAlbum = JsonConvert.DeserializeObject<Album>(tracks[j]["album"].ToString());

                                    if (artistDeezerId.Add(trackArtist.DeezerId))
                                    {
                                        artists.Add(trackArtist);
                                        trackAsObject.Artist = trackArtist;
                                        trackAlbum.Artist = trackArtist;
                                    }
                                    else
                                    {
                                        var temp = artists.FirstOrDefault(a => a.DeezerId == trackArtist.DeezerId);
                                        trackAsObject.Artist = temp;
                                        trackAlbum.Artist = temp;
                                    }

                                    if (albumDeezerId.Add(trackAlbum.DeezerId))
                                    {
                                        albums.Add(trackAlbum);
                                        trackAsObject.Album = trackAlbum;
                                    }
                                    else 
                                    {
                                        trackAsObject.Album = albums.FirstOrDefault(a => a.DeezerId == trackAlbum.DeezerId);
                                    }

                                    trackAsObject.Genre = genre;
                                    await _appDbContext.Tracks.AddAsync(trackAsObject);
                                    await _appDbContext.SaveChangesAsync();
                                }
                            }

                        }

                    }
                }
                await _appDbContext.Artists.AddRangeAsync(artists);
                await _appDbContext.Albums.AddRangeAsync(albums);

                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
