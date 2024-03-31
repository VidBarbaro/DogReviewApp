using AutoMapper;
using DogReviewAPI.Data;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;

namespace DogReviewAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfADog(int dogId)
        {
            return _context.Reviews.Where(r => r.Dog.Id == dogId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
