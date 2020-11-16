using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    /*To use SqlRestaurantData  inside our application instead of our inmemoryrestaurant data there is only one change we need to make inside startup.cs*/
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        /*in order for sqlrestaurantdata to do anything meaningfull it is going to need an instance of OdeToFoodDbContext
          and like everything in asp.net core we want to avoid constructing OTFDbContext ourselves instead ask for constructor parameter
         and initializing its field with its value "odeToFoodDbContext" and the beauty of all this is through magic of configure services
            ASP.Net core is going to come across some componens or razor page that needs something that implements IRestaurantData.
        We're going to configure in "SqlRestaurantData" to be injected for IRestaurantdata and ASP.Net core is going to see oh,the SqlRestaurant 
        Data needs a DbContext let me work with the entity framework to grab one of those and pass that into the constructor so there else i have to do 
        to receive a DbContext and when someone wants to add a new restaurant .This is a very simple operation with EF .
         */
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            /*We can also say db.add right off dbcontext cause EF is smart to say oh you're passing in an object of type restaurant 
             * You must have wanted to add this to the restaurant collection which is the restaurnats table inside of the db*/
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            /*With EF on the DbContext there's a method SaveChanges [Invoke this method -> it returns an integer which represents no of 
             * rows affected in the db so we retrun a value returned by SaveChanges()]*/
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            /*First go and find the restaurant to delete,if it exists in the db then want to go to restaurant dbset property on dbcontext 
             * tell it to remove the restaurant 
            And do wanna point out operations like db.Restaurants.Remove and db.Restaurants.Add nothing will change in the database untill someone
            calls commit thats when EF tries to reconisle all the changs that i've asked for with the db.So,It says you have added two restaurants and removed
            one ,Let me issue two insert statements and a delete statements that what happens during save changes */
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return db.Restaurants;
        }

        public Restaurant GetById(int id)
        {
            /*Simple operation with EF on the restaurant DBset or on every Dbset there is a find method where we can just pass in primary key
             or the key values for a particular entity and the EF will figure out how to track down the entity inside of the db .If it will not find the
            restaurant it will return null.*/
            return db.Restaurants.Find(id);
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            /*Linq Query 
            These expressions are c# code that the EF can translate into a sql query against the database so all the heavy work occurs 
            inside the database including the orderby and select */
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            /*first of all going to use a method on the restaurants dbset which is attach method and what i want to attach is this updated Restaurant
             * so attach is a way of telling the entity Framework I'm about to give you an object that represents info that is already in db 
             but i want to give you the object because i ant you to start tracking changes about this object ,So not adding a new restaurant not trying to retrieve 
            the restaurant from the db.I already have all the restaurant data here inside of an entity object and i'm just going to attach it to the DbContext 
            so that the DbContext can manage that particular restaurant entity.then i will tell EF state of this particular entity is Modified */
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;

        }
    }
}
