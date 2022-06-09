using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int genreId);
        ICollection<Album> GetAlbumsByGenre(int genreId);
        bool GenreExists(int genreId);
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool Save();
    }
}
