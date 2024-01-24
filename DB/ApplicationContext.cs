using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using prog3_kursach.Model;
using System;

namespace prog3_kursach
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<AlbumTrack> AlbumsTracks { get; set; } = null!;
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<PlaylistTrack> PlaylistsTracks { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}