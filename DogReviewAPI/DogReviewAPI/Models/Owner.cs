namespace DogReviewAPI.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Town { get; set; }
        public Country Country { get; set; }
        public ICollection<DogOwner> DogOwners { get; set;}
    }
}
