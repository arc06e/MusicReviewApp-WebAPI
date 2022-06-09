namespace MusicReviewAppAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string MusicGenre { get; set; }
        //NP
        public ICollection<AlbumGenre> AlbumGenres { get; set; }

    }
}
