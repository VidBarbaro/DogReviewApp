namespace DogReviewAPI.Models
{
    // join table
    public class DogBreed
    {
        public int DogId { get; set; }
        public int BreedId { get; set; }
        public Dog Dog { get; set; }
        public Breed Breed { get; set; }
    }
}
