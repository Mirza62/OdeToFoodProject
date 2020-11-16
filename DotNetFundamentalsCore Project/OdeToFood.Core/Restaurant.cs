using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToFood.Core
{
    /// <summary>
    /// IValidatanleObject is to be inherited for complex validations by Restaurant
    /// </summary>
    public class Restaurant
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public String Name { get; set; }

        [Required, StringLength(255)]
        public String Location { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}