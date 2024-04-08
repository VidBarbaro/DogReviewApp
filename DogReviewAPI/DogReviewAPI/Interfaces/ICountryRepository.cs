using DogReviewAPI.Models;

namespace DogReviewAPI.Interfaces
{
    public interface ICountryRepository
    {
        // get
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromCountry(int countryId);
        bool CountryExists(int id);

        // post
        bool CreateCountry(Country country);

        // save
        bool Save();

        // put
        bool UpdateCountry(Country country);

        // delete
        bool DeleteCountry(Country country);
    }
}
