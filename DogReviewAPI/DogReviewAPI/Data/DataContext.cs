using DogReviewAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DogReviewAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // here we're telling the context what all out tables are
        public DbSet<Breed> Breeds {  get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogOwner> DogsOwners { get; set; }
        public DbSet<DogBreed> DogsBreeds { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // we need to tell the entity framework to link these 2 IDs together since they have a relationship
            // otherwise entity framework doesn't know
            modelBuilder.Entity<DogBreed>()
                .HasKey(db => new { db.DogId, db.BreedId });
            modelBuilder.Entity<DogBreed>()
                .HasOne(d => d.Dog)
                .WithMany(db => db.DogBreeds)
                .HasForeignKey(d => d.DogId);
            modelBuilder.Entity<DogBreed>()
                .HasOne(d => d.Breed)
                .WithMany(db => db.DogBreeds)
                .HasForeignKey(b => b.BreedId);

            modelBuilder.Entity<DogOwner>()
                .HasKey(dow => new { dow.DogId, dow.OwnerId });
            modelBuilder.Entity<DogOwner>()
                .HasOne(d => d.Dog)
                .WithMany(dow => dow.DogOwners)
                .HasForeignKey(d => d.DogId);
            modelBuilder.Entity<DogOwner>()
                .HasOne(d => d.Owner)
                .WithMany(dow => dow.DogOwners)
                .HasForeignKey(d => d.OwnerId);
        }
    }
}
