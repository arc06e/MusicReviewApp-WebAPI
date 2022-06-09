namespace MusicReviewAppAPI.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public int YearReleased { get; set; }
        //NP
        public ICollection<Review> Reviews { get; set; }
        public ICollection<AlbumGenre> AlbumGenres { get; set; }
        public ICollection <AlbumArtist> ArtistAlbums { get; set; }
    }
}
