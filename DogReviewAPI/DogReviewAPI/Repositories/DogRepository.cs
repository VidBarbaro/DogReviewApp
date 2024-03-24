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

        public ICollection<Dog> GetDogs()
        {
            return _context.Dogs.OrderBy(d => d.Id).ToList();
        }
    }
}
