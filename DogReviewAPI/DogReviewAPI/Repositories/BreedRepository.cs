using DogReviewAPI.Data;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;

namespace DogReviewAPI.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private DataContext _context;
        public BreedRepository(DataContext context)
        {
            _context = context;
        }
        public bool BreedExists(int breedId)
        {
            return _context.Breeds.Any(b =>  b.Id == breedId);
        }

        public Breed GetBreed(int id)
        {
            return _context.Breeds.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Breed> GetBreeds()
        {
            // order by names and converted to list (because we're returing an ICollection)
            return _context.Breeds.OrderBy(b => b.Name).ToList();
        }

        public ICollection<Dog> GetDogsByBreed(int breedId)
        {
            return _context.DogsBreeds.Where(b => b.BreedId == breedId).Select(d => d.Dog).ToList();
        }
    }
}
