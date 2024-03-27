using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface IBreedRepository
    {
        ICollection<Breed> GetBreeds();
        Breed GetBreed(int id);
        ICollection<Dog> GetDogsByBreed(int breedId);
        bool BreedExists(int breedId);
    }
}
