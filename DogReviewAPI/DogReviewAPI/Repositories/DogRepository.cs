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

        public bool CreateDog(int ownerId, int breedId, Dog dog)
        {
            var dogOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var breed = _context.Breeds.Where(b => b.Id == breedId).FirstOrDefault();

            // since Dog has 2 join table relationships we have to create the
            // necessary data that goes in the join tables
            var dogOwner = new DogOwner()
            {
                Owner = dogOwnerEntity,
                Dog = dog,
            };

            _context.Add(dogOwner);

            var dogBreed = new DogBreed()
            {
                Breed = breed,
                Dog = dog,
            };

            _context.Add(dogBreed);

            _context.Add(dog);

            return Save();
        }

        public bool DeleteDog(Dog dog)
        {
            _context.Remove(dog);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDog(Dog dog)
        {
            // var dogOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            //var breed = _context.Breeds.Where(b => b.Id == breedId).FirstOrDefault();
           _context.Update(dog);
            return Save();
        }
    }
}
