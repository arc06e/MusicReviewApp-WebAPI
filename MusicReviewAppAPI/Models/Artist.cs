namespace MusicReviewAppAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        //NP
        public Country Country { get; set; }
        public ICollection<AlbumArtist> ArtistAlbums { get; set; }
    }
}
