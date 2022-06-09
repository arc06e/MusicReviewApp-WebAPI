using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Interfaces
{
    public interface IArtistRepository
    {
        ICollection<Artist> GetArtists();
        //with read methods, you use ids not not objects
        Artist GetArtist(int artistId);
        bool ArtistExists(int artistId);

        ICollection<Album> GetAlbumsByArtist(int artistId);

        //with create methods, you add objects to db via EF
        bool CreateArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artist);
        bool Save();
    }
}
