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
            CreateMap<Dog, DogDto>().ReverseMap();
            CreateMap<Breed, BreedDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Reviewer, ReviewerDto>().ReverseMap();
        }
    }
}
