using DogReviewAPI.Data;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;

namespace DogReviewAPI.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly DataContext _context;

        public DogRepository(DataContext context)
        {
            _context = context;
        }

        public bool DogExists(int id)
        {
            return _context.Dogs.Any(d => d.Id == id);
        }

        public Dog GetDog(int id)
        {
            return _context.Dogs.Where(d => d.Id == id).FirstOrDefault();
        }

        public Dog GetDog(string name)
        {
            return _context.Dogs.Where(d => d.Name == name).FirstOrDefault();
        }

        public decimal GetDogRating(int id)
        {
            var reviews = _context.Reviews.Where(d => d.Dog.Id == id);
            if(reviews.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Dog> GetDogs()
        {
            return _context.Dogs.OrderBy(d => d.Id).ToList();
        }
    }
}
