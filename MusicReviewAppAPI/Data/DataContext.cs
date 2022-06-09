using Microsoft.EntityFrameworkCore;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<AlbumArtist> AlbumArtists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumGenre> AlbumGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<AlbumArtist>()
                .HasKey(aa => new { aa.ArtistId, aa.AlbumId });
            modelBuilder.Entity<AlbumArtist>()
                .HasOne(a => a.Artist)
                .WithMany(aa => aa.ArtistAlbums)
                .HasForeignKey(a => a.ArtistId);
            modelBuilder.Entity<AlbumArtist>()
                .HasOne(a => a.Album)
                .WithMany(aa => aa.ArtistAlbums)
                .HasForeignKey(a => a.AlbumId);

            modelBuilder.Entity<AlbumGenre>()
                .HasKey(ag => new { ag.AlbumId, ag.GenreId });
            modelBuilder.Entity<AlbumGenre>()
                .HasOne(a => a.Album)
                .WithMany(ag => ag.AlbumGenres)
                .HasForeignKey(a => a.AlbumId);
            modelBuilder.Entity<AlbumGenre>()
                .HasOne(g => g.Genre)
                .WithMany(ag => ag.AlbumGenres)
                .HasForeignKey(g => g.GenreId);
        }
    }
}
