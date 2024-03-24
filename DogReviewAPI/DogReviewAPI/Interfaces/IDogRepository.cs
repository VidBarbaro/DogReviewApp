using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface IDogRepository
    {
        ICollection<Dog> GetDogs();
    }
}
