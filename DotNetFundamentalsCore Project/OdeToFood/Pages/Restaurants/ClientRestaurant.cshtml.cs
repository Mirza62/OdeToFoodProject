using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Api;

namespace OdeToFood.Pages.Restaurants
{
    public class ClientRestaurantModel : PageModel
    {
        private readonly RestaurantsController restaurantsController;

        public void OnGet()
        {

        }
    }
}
/*We can do this using react,angular or aurelia or using vue.js any number of different client technologies
 we are going to keep everything simple and use jquery and a jquery plug in  we are going to demonstrate 
How to Build an API endpoint inside of asp.net core,
How to Nivoke that API from JAvaScript and 
use the endpoint to fetch data in JSON Format   that is what client restaurant is going to do wern't going to do 
any server processing I'me going to need an OnGet method or PageModel At All.

 */
