using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants { get; set; }

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1 , Name = "FourSeason" , Location ="Hyderabad", Cuisine = CuisineType.American},
                new Restaurant {Id = 2, Name = "Azeebos", Location = "Mehdipatnam",Cuisine = CuisineType.Arabian},
                new Restaurant {Id = 3, Name = "Bawarchi", Location = "Jangaon", Cuisine = CuisineType.Chinese},
                new Restaurant {Id = 4, Name = "Mazedar", Location = "Mars", Cuisine = CuisineType.Indian},
                new Restaurant {Id = 5, Name = "Green Park", Location = "Warangal", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 6, Name = "Mataam al mandi", Location = "Airport", Cuisine = CuisineType.None},
            };
        }


        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants orderby r.Name select r;
        }

        /// <summary>
        /// Code Refactoring by using getall and getrestaurantbyname in the same method 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            ////return from r in restaurants orderby r.Name select r;
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var RestaurantDb = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (RestaurantDb != null)
            {
                RestaurantDb.Name = updatedRestaurant.Name;
                RestaurantDb.Location = updatedRestaurant.Location;
                RestaurantDb.Cuisine = updatedRestaurant.Cuisine;
            }

            return RestaurantDb;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
