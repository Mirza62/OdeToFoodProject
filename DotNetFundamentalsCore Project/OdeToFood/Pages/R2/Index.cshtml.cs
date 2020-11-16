using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.R2
{
    public class IndexModel : PageModel
    {
        private readonly OdeToFood.Data.OdeToFoodDbContext _context;

        public IndexModel(OdeToFood.Data.OdeToFoodDbContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get;set; }

        public async Task OnGetAsync()
        {
            Restaurant = await _context.Restaurants.ToListAsync();
        }
    }
}
/*
        Now that we've built out this application that is using the entity framework to talk to a database and we have views
        that will allow me to list restaurants , details restaurants , edit, update, delete restaurants 'let me show you a feature
        known as scafolding tools on both the command line interface and in vs.
        we could have code generated most of the functionality just by setting up our entity which is the restaurant and our dbcontext
        this can be helpful when we need to move quickly and to get a prototype in place for someone to use
        
    
    Demo -> go to pages->Add new Folder-> "R2"->RC Add new Scaffolding item -> select Razor Pages using entity framework (CRUD)
    Model clas -> Restaurant.cs DataContext Class -> OdeToFoodDbContext.cs   
    untick ->Partial Views Tick -> Reference script libraries Tick-> Use aLayout page , Leave Empty Fill in the blank for _ViewStartFile 
    to insert a layout file.click okay
Go to localhost:65001/r2 


     All of the work done in the Restaurants folder can also be done by scaffolding here is an example
*/
