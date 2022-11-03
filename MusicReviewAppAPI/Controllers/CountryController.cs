using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicReviewAppAPI.DTO;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId) 
        {
            if (!_countryRepository.CountryExists(countryId)) 
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpGet("artists/{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistsFromACountry(int countryId) 
        {
            if (!_countryRepository.CountryExists(countryId)) 
            {
                return NotFound();
            }
            var artists = _mapper.Map<List<ArtistDto>>(_countryRepository.GetArtistsFromACountry(countryId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(artists);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate) 
        {
            if (countryCreate == null) 
            {
                return BadRequest(ModelState);
            }

            var country = _countryRepository.GetCountries().Where(
                c => c.CountryName.Trim().ToUpper() == countryCreate.CountryName.TrimEnd().ToUpper()).FirstOrDefault();

            if (country != null) 
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countryMap)) 
            {
                ModelState.AddModelError("", "Something went wrong while updating Country");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null) 
            {
                return BadRequest(ModelState);
            }
                        
            if (countryId != updatedCountry.Id) 
            {       
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CountryExists(countryId)) 
            {
                return NotFound();
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!_countryRepository.UpdateCountry(countryMap)) 
            {
                ModelState.AddModelError("", "Something went wrong with updating country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId)) 
            {
                return NotFound();
            }

            var countryToDelete = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.DeleteCountry(countryToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong with deleting country");
            }

            return NoContent();
        }

    }
}
