using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    /*A View Component in asp.net core derives from a base class which is the view component class
     a view component just like the page model in a razor page can have constructor or i do dependency
      injections
    the difference between razor page and viewcomponent is A viewcompnent doesnt respond to an http request
     A view component is more like a partial view it is going to embedded in some other view that is simply going 
    say please go render this view component so when the rendering happens asp.net core is going to call a method 
    named "Invoke" 
    I have this component "RestaurantCountViewComponent" this service that i can use from a layout view it can do
    its own data access using whatever services it needs it could build its own model and it can render its own view which 
    will plug into the layout view.
     */
    public class RestaurantCountViewComponent : ViewComponent
    {
        private readonly IRestaurantData restaurantData;


        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        /*One thing about this operation ,this view component if i place it onto the layout page it's going to add
         a query to every page in the app[if you're in a performance sensitive scenario you might want to cache this 
         value]but we are just going to focus on view component
        unlike a page where i can simply say return page to render the associated cshtml file i want to explicitly say i 
        want to return View() so somewhere in the project a view might represent the view for this view component.
        View components works more like a mvc framework inside of the asp.net core 
        you have an action method builds a model and you pass that model to a view by returning some sort of view result
        in a razor page i would simply set that count as a property on my page and then bind to it from the cshtml file.
         */
        public IViewComponentResult Invoke()
        {
            int count = restaurantData.GetCountOfRestaurants();
            return View(count);
        }
    }
}
