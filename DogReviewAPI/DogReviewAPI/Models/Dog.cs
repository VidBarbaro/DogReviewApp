﻿namespace DogReviewAPI.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<DogOwner> DogOwners { get; set;}
        public ICollection<DogBreed> DogBreeds { get; set; }
    }
}
