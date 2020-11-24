using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ridepal.Tests;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services;
using RidePal.Services.DTOModels;
using System;
using System.Threading.Tasks;

namespace PlaylistGenerator.Services.Tests.ArtistServiceTests
{
    [TestClass]
    public class GetArtistByIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectArtist()
        {
            //Arrange 
            var options = Utils.GetOptions(nameof(ReturnCorrectArtist));

            using (var arrangeContext = new AppDbContext(options))
            {
                await arrangeContext.Artists.AddAsync(new Artist
                {
                    Name = "Name",
                    DeezerId = "DeezerId",
                    Picture = "PictureUrl",
                    TrackListURL = "URL",
                });

                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new AppDbContext(options))
            {
                var expectedArtist = Utils.Mapper.Map<ArtistDTO>(await actContext.Artists.FirstAsync());
                var sut = new ArtistService(actContext, Utils.Mapper);

                //Act
                var actualArtist = await sut.GetArtistAsync(expectedArtist.Id);

                //Assert
                Assert.AreEqual(expectedArtist.Id, actualArtist.Id);
                Assert.AreEqual(expectedArtist.Name, actualArtist.Name);
                Assert.AreEqual(expectedArtist.Picture, actualArtist.Picture);
                Assert.AreEqual(expectedArtist.TrackListURL, actualArtist.TrackListURL);
            }
        }

        [TestMethod]
        public async Task Throw_When_ArtistNotFound()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_ArtistNotFound));

            var context = new AppDbContext(options);
            var sut = new ArtistService(context, Utils.Mapper);

            //Act & Assert
            Assert.IsNull(await sut.GetArtistAsync(Guid.NewGuid()));
        }
    }
}
