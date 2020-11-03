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
            playlist
                .HasKey(trackUser => trackUser.UserId);

            playlist
                .HasOne(playlist => playlist.User)
                .WithMany(user => user.Playlists)
                .HasForeignKey(playlist => playlist.UserId);

        }
    }

}
