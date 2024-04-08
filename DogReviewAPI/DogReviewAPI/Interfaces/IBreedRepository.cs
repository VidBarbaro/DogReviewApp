using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface IBreedRepository
    {
        // get
        ICollection<Breed> GetBreeds();
        Breed GetBreed(int id);
        ICollection<Dog> GetDogsByBreed(int breedId);
        bool BreedExists(int breedId);

        // post
        bool CreateBreed(Breed breed);

        // save
        bool Save();

        // put
        bool UpdateBreed(Breed breed);

        // delete
        bool DeleteBreed(Breed breed);
    }
}
