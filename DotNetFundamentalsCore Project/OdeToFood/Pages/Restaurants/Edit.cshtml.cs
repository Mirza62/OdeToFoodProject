using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;


        /// <summary>
        /// When user goes to the form and make edit and save the form it must also act as both input as well as output
        /// </summary>
        [BindProperty]
        public Restaurant Restaurant { get; set; }

        /// <summary>
        /// In order to use dropdownlist of cuisine in view and for using html adding it in constructor 
        /// </summary>
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData ,IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }


        public IActionResult OnGet(int? restaurantId)
        {
            ////for building ienumerable of select list items in order to use it in drop downlist.
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant == null)
                return RedirectToPage("./NotFound");
            return Page();
        }

        /// <summary>
        /// No input paramters cause Restaurants have been attributed as bindproperty it act as input as well as output so.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            ////Using ModelState to check the validation provided by attributes in the model class.e.g : ModelState["Location"].Errors/.Attemptedvalues ,
            if (!ModelState.IsValid)
            {
                ////For Cuisines to show after saving we need to give the data.
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
            }
            else
            {
                restaurantData.Add(Restaurant);
            }

            restaurantData.Commit();
            ////TempData is the dictionary of keyvalue pairs 
            TempData["Message"]= "A New Restaurant has been Added";
            ////This Pattern is called PRG Post/Redirect/Get[Mostly used for form Post Operations]
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id } );
        }
    }
}
