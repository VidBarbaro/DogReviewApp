namespace DogReviewAPI.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DogBreed> DogBreeds { get; set; }
    }
}
