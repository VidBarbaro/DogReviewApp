﻿using DogReviewAPI.Data;
using DogReviewAPI.Interfaces;
using DogReviewAPI.Models;

namespace DogReviewAPI.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private DataContext _context;
        public OwnerRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Dog> GetDogsByOwner(int ownerId)
        {
            return _context.DogsOwners.Where(o => o.Owner.Id == ownerId).Select(d => d.Dog).ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfADog(int dogId)
        {
            return _context.DogsOwners.Where(d => d.Dog.Id == dogId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }
    }
}