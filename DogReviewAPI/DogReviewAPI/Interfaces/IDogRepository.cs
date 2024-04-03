using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface IDogRepository
    {
        ICollection<Dog> GetDogs();
        Dog GetDog(int id);
        Dog GetDog(string name);
        decimal GetDogRating(int id);
        bool DogExists(int id);
        bool CreateDog(int ownerId, int breedId, Dog dog);
        bool Save();
    }
}
