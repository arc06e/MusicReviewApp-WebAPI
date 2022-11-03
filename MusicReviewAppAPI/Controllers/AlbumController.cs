using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicReviewAppAPI.DTO;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
        public IActionResult GetAlbums()
        {
            var albums = _mapper.Map<List<AlbumDto>>(_albumRepository.GetAlbums());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(albums);
        }

        [HttpGet("{albumId}")]
        [ProducesResponseType(200, Type = typeof(Album))]
        [ProducesResponseType(400)]
        public IActionResult GetAlbum(int albumId)
        {
            if (!_albumRepository.AlbumExists(albumId))
            {
                return NotFound();
            }

            var album = _mapper.Map<AlbumDto>(_albumRepository.GetAlbum(albumId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(album);
        }
      
        [HttpGet("artist/{albumId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistsByAlbumId(int albumId)
        {
            var artists = _mapper.Map<List<ArtistDto>>(_albumRepository.GetArtistsByAlbum(albumId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(artists);
        }

        [HttpGet("{albumId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetAlbumRating(int albumId)
        {
            if (!_albumRepository.AlbumExists(albumId))
            {
                return NotFound();
            }

            var rating = _albumRepository.GetAlbumRating(albumId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAlbum([FromQuery] int artistId, int genreId, [FromBody] AlbumDto albumCreate)
        {
            if (albumCreate == null)
            {
                return BadRequest(ModelState);
            }

            var album = _albumRepository.GetAlbums()
                .Where(x => x.AlbumName.Trim().ToUpper() == albumCreate.AlbumName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (album != null)
            {
                ModelState.AddModelError("", "Album already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var albumMap = _mapper.Map<Album>(albumCreate);

            if (!_albumRepository.CreateAlbum(artistId, genreId, albumMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{albumId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAlbum(int albumId,
            int artistId, int genreId, [FromBody] AlbumDto updatedAlbum)
        {
            if (albumId == null)
            {
                return BadRequest(ModelState);
            }

            if (albumId != updatedAlbum.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_albumRepository.AlbumExists(albumId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var albumMap = _mapper.Map<Album>(updatedAlbum);

            if (!_albumRepository.UpdateAlbum(artistId, genreId, albumMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating album");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{albumId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAlbum(int albumId) 
        {
            if (!_albumRepository.AlbumExists(albumId)) 
            {
                return NotFound();
            }

            var albumToDelete = _albumRepository.GetAlbum(albumId);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!_albumRepository.DeleteAlbum(albumToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong with deleting album");
            }

            return NoContent();
        }
    }
}
