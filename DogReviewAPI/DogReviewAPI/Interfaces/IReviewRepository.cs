﻿using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfADog(int dogId);
        bool ReviewExists(int reviewId);
    }
}
