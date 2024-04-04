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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBreed([FromBody] BreedDto breedCreate)
        {
            if(breedCreate == null)
            {
                return BadRequest();
            }

            var breed = _breedRepository.GetBreeds()
                .Where(b => b.Name.Trim().ToUpper() == breedCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (breed != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var breedMap = _mapper.Map<Breed>(breedCreate);

            if (!_breedRepository.CreateBreed(breedMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{breedId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBreed(int breedId, [FromBody] BreedDto updatedBreed)
        {
            if (updatedBreed == null)
            {
                return BadRequest(ModelState);
            }

            if (breedId != updatedBreed.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_breedRepository.BreedExists(breedId))
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var breedMap = _mapper.Map<Breed>(updatedBreed);

            if (!_breedRepository.UpdateBreed(breedMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
