namespace MusicReviewAppAPI.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        //NP
        public ICollection<Artist> Artists { get; set; }
    }
}
