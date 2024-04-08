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

        public bool CreateBreed(Breed breed)
        {
            // Change Tracker
            // is it adding, updating, modifying...
            // can be connected(99% of the time) or disconnected
            _context.Add(breed);
            // this Save function creates the SQL query and executes it in the database
            return Save();
        }

        public bool DeleteBreed(Breed breed)
        {
            _context.Remove(breed);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBreed(Breed breed)
        {
            _context.Update(breed);
            return Save();
        }
    }
}
