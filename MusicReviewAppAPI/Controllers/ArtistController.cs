using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicReviewAppAPI.DTO;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public ArtistController(IArtistRepository artistRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        public IActionResult GetArtists()
        {
            var artists = _mapper.Map<List<ArtistDto>>(_artistRepository.GetArtists());

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(artists);
        }

        [HttpGet("{artistId}")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        public IActionResult GetArtist(int artistId) 
        {
            if (!_artistRepository.ArtistExists(artistId)) 
            {
                return NotFound(); 
            }

            var artist = _mapper.Map<ArtistDto>(_artistRepository.GetArtist(artistId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(artist);
        }

        [HttpGet("{artistId}/Albums")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        public IActionResult GetAlbumsByArtist(int artistId) 
        {
            if (!_artistRepository.ArtistExists(artistId)) 
            {
                return NotFound();
            }

            var albums = _mapper.Map<List<AlbumDto>>(_artistRepository.GetAlbumsByArtist(artistId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(albums);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]                  //needed because of fk
        public IActionResult CreateArtist([FromQuery] int countryId, [FromBody] ArtistDto artistCreate) 
        {
            if(artistCreate == null) 
            {
                return BadRequest(ModelState);
            }

            var artist = _artistRepository.GetArtists()
                .Where(a => a.ArtistName.Trim().ToUpper() == artistCreate.ArtistName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (artist != null) 
            {
                ModelState.AddModelError("", "Artist already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }           

           //needed because of fk
           var artistMap = _mapper.Map<Artist>(artistCreate);
           artistMap.Country = _countryRepository.GetCountry(countryId);

            if (!_artistRepository.CreateArtist(artistMap)) 
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{artistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateArtist(int artistId, [FromBody] ArtistDto updatedArtist)
        {
            if (updatedArtist == null) 
            {
                return BadRequest(ModelState);
            }

            if (artistId != updatedArtist.Id) 
            {
                return BadRequest(ModelState);
            }

            if (!_artistRepository.ArtistExists(artistId)) 
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artistMap = _mapper.Map<Artist>(updatedArtist);

            if (!_artistRepository.UpdateArtist(artistMap)) 
            {
                ModelState.AddModelError("", "Something went wrong with updating artist");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }

        [HttpDelete("{artistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteArtist(int artistId) 
        {
            if (!_artistRepository.ArtistExists(artistId)) 
            {
                return NotFound();
            }

            var artistToDelete = _artistRepository.GetArtist(artistId);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!_artistRepository.DeleteArtist(artistToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong deleting artist");
            }

            return NoContent();
        }

    }
}
