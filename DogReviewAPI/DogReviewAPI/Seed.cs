using DogReviewAPI.Data;
using DogReviewAPI.Models;

namespace DogReviewAPI
{
    // script that prepopulates the database with data
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.DogsOwners.Any())
            {
                var dogOwners = new List<DogOwner>()
                {
                    new DogOwner()
                    {
                        Dog = new Dog()
                        {
                            Name = "Lava",
                            BirthDate = new DateTime(2008,1,1),
                            DogBreeds = new List<DogBreed>()
                            {
                                new DogBreed { Breed = new Breed() { Name = "Lhasa Apso"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Lava cool!",Text = "Lava is the best dog, because it is a Lhasa Apso", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Nice dog!", Text = "Lava is the best a running", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Lovely",Text = "Lava, Lava, Lava", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Vid",
                            Town = "Rovinj",
                            Country = new Country()
                            {
                                Name = "Croatia"
                            }
                        }
                    },
                    new DogOwner()
                    {
                        Dog = new Dog()
                        {
                            Name = "Max",
                            BirthDate = new DateTime(2010,12,1),
                            DogBreeds = new List<DogBreed>()
                            {
                                new DogBreed { Breed = new Breed() { Name = "Mix"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Max", Text = "Max is the best dog, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Max",Text = "Max is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Max", Text = "Max, Max, Max", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Harry",
                            Town = "London",
                            Country = new Country()
                            {
                                Name = "England"
                            }
                        }
                    },
                                    new DogOwner()
                    {
                        Dog = new Dog()
                        {
                            Name = "Bea",
                            BirthDate = new DateTime(1903,1,1),
                            DogBreeds = new List<DogBreed>()
                            {
                                new DogBreed { Breed = new Breed() { Name = "Malteser"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Bea",Text = "Bea is the best dog, because it is fast", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Bea",Text = "Bea is the best a killing birds", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Bea",Text = "Bea, Bea, Bea", Rating = 3,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Maya",
                            Town = "Eindhoven",
                            Country = new Country()
                            {
                                Name = "Netherlands"
                            }
                        }
                    }
                };
                dataContext.DogsOwners.AddRange(dogOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
