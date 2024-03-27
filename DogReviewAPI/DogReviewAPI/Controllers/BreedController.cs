using AutoMapper;
using DogReviewAPI.Dto;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : Controller
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;
        public BreedController(IBreedRepository breedRepository, IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Breed>))]
        public IActionResult GetBreeds() 
        {
            var breeds = _mapper.Map<List<BreedDto>>(_breedRepository.GetBreeds());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(breeds);
        }

        [HttpGet("{breedId}")]
        [ProducesResponseType(200, Type = typeof(Breed))]
        public IActionResult GetBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();

            var breed = _mapper.Map<BreedDto>(_breedRepository.GetBreed(breedId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(breed);
        }

        [HttpGet("dog/{breedId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]
        [ProducesResponseType(400)]
        public IActionResult GetDogsByBreed(int breedId)
        {
            var dogs = _mapper.Map<List<DogDto>>(_breedRepository.GetDogsByBreed(breedId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dogs);
        }
    }
}
