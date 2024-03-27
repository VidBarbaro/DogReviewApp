using AutoMapper;
using DogReviewAPI.Dto;
using DogReviewAPI.Models;

namespace DogReviewAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Dog, DogDto>();
        }
    }
}
