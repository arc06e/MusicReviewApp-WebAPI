using MusicReviewAppAPI.Data;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DataContext _context;

        public ArtistRepository(DataContext context)
        {
            _context = context;
        }

        public bool ArtistExists(int artistId)
        {
            return _context.Artists.Any(a => a.Id == artistId);
        }

        public ICollection<Artist> GetArtists()
        {
            return _context.Artists.ToList();
        }

        public Artist GetArtist(int artistId)
        {
            return _context.Artists.Where(a => a.Id == artistId).FirstOrDefault();
        }

        public ICollection<Album> GetAlbumsByArtist(int artistId) 
        {
            return _context.AlbumArtists.Where(a => a.Artist.Id == artistId).Select(a => a.Album).ToList();
        }


        public bool CreateArtist(Artist artist)
        {

            _context.Add(artist);
            return Save();
        }

        public bool UpdateArtist(Artist artist)
        {
            _context.Update(artist);
            return Save();
        }

        public bool DeleteArtist(Artist artist)
        {
            _context.Remove(artist);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
