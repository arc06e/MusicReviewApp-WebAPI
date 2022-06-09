using MusicReviewAppAPI.Data;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DataContext _context;

        public AlbumRepository(DataContext context)
        {
            _context = context;
        }

        public bool AlbumExists(int id)
        {
            return _context.Albums.Any(a => a.Id == id);
        }

        public ICollection<Album> GetAlbums()
        {
            return _context.Albums.ToList();
        }

        public ICollection<Artist> GetArtistsByAlbum(int albumId)
        {                                                                                     //because an album can have multiple artists           
            return _context.AlbumArtists.Where(x => x.AlbumId == albumId).Select(a => a.Artist).ToList();
        }

        public decimal GetAlbumRating(int albumId) 
        {
            var review = _context.Reviews.Where(p => p.Album.Id == albumId);
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool CreateAlbum(int artistId, int genreId, Album album)
        {
            var artistEntity = _context.Artists.Where(a => a.Id == artistId).FirstOrDefault();
            var genreEntity = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();

            var albumArtist = new AlbumArtist 
            {
                Album = album,
                Artist = artistEntity
            };

            _context.Add(albumArtist);

            var albumGenre = new AlbumGenre
            {
                Album = album,
                Genre = genreEntity
            };

            _context.Add(albumGenre);
            _context.Add(album);

            return Save();
        }

        public Album GetAlbum(int id)
        {
            return _context.Albums.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool UpdateAlbum(int artistId, int genreId, Album album)
        {
            _context.Update(album);
            return Save();
        }

        public bool DeleteAlbum(Album album)
        {
            _context.Remove(album);
            return Save();
        }               

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
