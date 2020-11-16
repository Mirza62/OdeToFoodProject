using OdeToFood.Core;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

        IEnumerable<Restaurant> GetRestaurantByName(string name);

        Restaurant GetById(int id);

        Restaurant Update(Restaurant updatedRestaurant);

        Restaurant Add(Restaurant newRestaurant);

        Restaurant Delete(int id);

        int GetCountOfRestaurants();

        /// <summary>
        /// Many Data Sources act as units of work they collect all the changes and additions and deletions to the data source
        /// and then they reconsile those changes when you invoke a method like commit or savechanges or commit transactions in EF
        /// After they make a change to an entity ,We can have EF Persist those changes in db by calling savechanges or _dbcontext
        /// we will see how that works later so Exposing same sort of functionality through this interface by having a commit method.
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}
