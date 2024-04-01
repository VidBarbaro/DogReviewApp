using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface IOwnerRepository
    {
        // get
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfADog(int dogId);
        ICollection<Dog> GetDogsByOwner(int ownerId);
        bool OwnerExist(int ownerId);

        // post
        bool CreateOwner(Owner owner);
        bool Save();
    }
}
