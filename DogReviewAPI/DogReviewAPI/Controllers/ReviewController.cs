using AutoMapper;
using DogReviewAPI.Dto;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IDogRepository dogRepository,IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(review);
        }

        [HttpGet("reviews/{dogId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfADog(int dogId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfADog(dogId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, int dogId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }

            var reviews = (_reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper())
                .FirstOrDefault());

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            reviewMap.Dog = _dogRepository.GetDog(dogId);

            if (!_reviewRepository.CreateReview( reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }
    }
}
