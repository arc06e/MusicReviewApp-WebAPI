using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicReviewAppAPI.DTO;
using MusicReviewAppAPI.Interfaces;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IAlbumRepository _albumRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, 
            IMapper mapper,
            IAlbumRepository albumRepository,
            IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _albumRepository = albumRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId) 
        {
            if (!_reviewRepository.ReviewExists(reviewId)) 
            {
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(review);
        }

        [HttpGet("album/{albumId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAnAlbum(int albumId)
        {
            if (!_albumRepository.AlbumExists(albumId)) 
            {
                return NotFound();
            }

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAnAlbum(albumId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpPost]        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, int albumId, [FromBody] ReviewDto createReview)
        {
            if (createReview == null) 
            {
                return BadRequest(ModelState);
            }

            var reviews = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == createReview.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null) 
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(createReview);

            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            reviewMap.Album = _albumRepository.GetAlbum(albumId);

            if (!_reviewRepository.CreateReview(reviewMap)) 
            {
                ModelState.AddModelError("", "Something went wrong with creating review");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview) 
        {
            if (updatedReview == null) 
            {
                return BadRequest(ModelState);
            }

            if (reviewId != updatedReview.Id) 
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap)) 
            {
                ModelState.AddModelError("", "Something went wrong with updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId) 
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var reviewToDelete = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.DeleteReview(reviewToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong with deleting review");
            }

            return NoContent();
        }
    }
}
