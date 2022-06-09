using MusicReviewAppAPI.Data;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        
        public ICollection<Country> GetCountries() 
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int countryId) 
        {
            return _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public ICollection<Artist> GetArtistsFromACountry(int countryId)
        {
            return _context.Artists.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool CountryExists(int countryId) 
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }

        public bool CreateCountry(Country country) 
        {
            _context.Add(country);
            return Save();
        }

        public bool UpdateCountry(Country country) 
        {
            _context.Update(country);
            return Save();
        }

        public bool DeleteCountry(Country country) 
        {
            _context.Remove(country);
            return Save();
        }

        public bool Save() 
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
