using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RidePal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Data.Configurations
{
    public class PlaylistConfig : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> playlist)
        {

            playlist.HasKey(trackUser => new { trackUser.UserId, trackUser.TrackId });

            playlist
                .HasOne(playlist => playlist.Track)
                .WithMany(track => track.Playlists)
                .HasForeignKey(playlist=>playlist.TrackId);

            playlist
                .HasOne(playlist => playlist.User)
                .WithMany(user => user.Playlists)
                .HasForeignKey(playlist => playlist.UserId);
 
        }
    }

}
