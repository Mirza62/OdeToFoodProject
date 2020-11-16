using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    /// <summary>
    /// Remember the goal for a pagemodel class like a listmodel working with is to respond to an http request 
    /// like a get request 
    /// </summary>
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        /// <summary>
        /// Since .Net knows about IRestaurant Data(I shall be able to ask for an instance of something 
        /// that implements IRestaurantData)
        /// Creating and initializing a field and save that value in the constructor
        /// </summary>
        /// <param name="config"></param>
        public ListModel(IConfiguration config, IRestaurantData restaurantData, ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }
        public string Message { get; set; }

        public IEnumerable<Restaurant> Restaurants { get; set; }

        /// <summary>
        /// request comes looking for searchterm and by search term in get we will be able to get all the restaurants.
        /// </summary>
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }


        /// <summary>
        /// we should use model binding as input parameters if it string as it will not raise exceptions
        /// now,we need to use searchTerm as input as well as output to make it stay in search bar so that
        /// user can check what he gave the input in searchTerm.-> so add property[SearchTerm] and assign 
        /// have to assign Searchterm[property] = searchTerm(inputparamter) ->Easy way is if property works as
        /// input,output or both by adding attribute [Bindproperty].instead of input paramater asp.net core 
        /// goes looking for a string searchterm before invoking onget.but bindproperty only binds on Post method .
        /// </summary>
        public void OnGet()
        {
            logger.LogError("Executing ListModel");
            Restaurants = restaurantData.GetRestaurantByName(SearchTerm);
            Message = config["Message"];
        }
    }
}
