using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;

namespace prog3_kursach
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; } = null!;
        //public DbSet<Album> Albums { get; set; } = null!;
        //public DbSet<Playlist> Playlists { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=entities.db");
        }
    }
}