using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Interfaces
{
    public interface IAlbumRepository
    {
        ICollection<Album> GetAlbums();
        //with read methods, you use ids not not objects
        Album GetAlbum(int id);
        ICollection<Artist> GetArtistsByAlbum(int albumId);
        decimal GetAlbumRating(int albumId);
        bool AlbumExists(int id);
        //with create methods, you add objects to db via EF
        bool CreateAlbum(int artistId, int genreId, Album album);
        bool UpdateAlbum(int artistId, int genreId, Album album);
        bool DeleteAlbum(Album album);
        bool Save();
    }
}
