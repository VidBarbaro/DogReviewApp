using AutoMapper;
using DogReviewAPI.Dto;
using DogReviewAPI.Models;

namespace DogReviewAPI.Helper
{
    public class MappingProfiles : Profile
    {
        // mapping configurations
        public MappingProfiles()
        {
            CreateMap<Dog, DogDto>();
            CreateMap<Breed, BreedDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
