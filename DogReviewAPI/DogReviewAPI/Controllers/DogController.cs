﻿using AutoMapper;
using DogReviewAPI.Dto;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;
using DogReviewAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public DogController(IDogRepository dogRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]
        public IActionResult GetDogs()
        {
            var dogs = _mapper.Map<List<DogDto>>(_dogRepository.GetDogs());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dogs);
        }

        [HttpGet("{dogId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]
        public IActionResult GetDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
            {
                return NotFound();
            }
            
            var dog = _mapper.Map<DogDto>(_dogRepository.GetDog(dogId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dog);
        }

        [HttpGet("{dogId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetDogRating(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
            {
                return NotFound();
            }

            var rating = _dogRepository.GetDogRating(dogId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }

        // 204 returns no content
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDog([FromQuery] int ownerId, [FromQuery] int breedId, [FromBody] DogDto dogCreate)
        {
            if(dogCreate == null)
            {
                return BadRequest(ModelState);
            }

            var dogs = _dogRepository.GetDogs()
                .Where(d => d.Name.Trim().ToUpper() == dogCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (dogs != null)
            {
                ModelState.AddModelError("", "Dog already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dogMap = _mapper.Map<Dog>(dogCreate);

            if (!_dogRepository.CreateDog(ownerId, breedId, dogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{dogId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDog(int dogId, [FromBody] DogDto updatedDog)
        {
            if (updatedDog == null)
            {
                return BadRequest(ModelState);
            }

            if (dogId != updatedDog.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_dogRepository.DogExists(dogId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dogMap = _mapper.Map<Dog>(updatedDog);

            if (!_dogRepository.UpdateDog(dogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{dogId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfADog(dogId);
            var dogToDelete = _dogRepository.GetDog(dogId);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
                return BadRequest(ModelState);
            }

            if (!_dogRepository.DeleteDog(dogToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting dog");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
