using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}

////Working with the database in ef core is relatively easy all we need to do is implement a class that derives from an entity framework class
///named DbContext and then add properties to that class containing the information we want the ef to store in a database.
///EF can Create and manage a database for us using information that we provide in this dbcontext.once ef knows we want to store restaurant information 
///the framework can create and update the db to store all the info we need.framework can change the schema so that the database can store all the
///information available for the restaurant this is the feature of EF known as Migrations.in order to create a migrations we need to use .Net Commandline 
///interface.

/*Adding Connection Strings and Registering Services.
 * 
 * 
 During Development,I'm Going to store my connection string inside [AppSetting.Json   i.e : Configuration of our web application.]
Providing information to give DbContext a connection string to get to a localdb, a sql server.We're going to have single connection string ->Providing Key Value Pair 
                            "Message": "Hello from AppSettings.json",
                                "ConnectionStrings": {
                                        "OdeToFoodDb": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OdeToFood;Integrated Security=True;"
                                     }

 OdeToFoodDb -> Name of the connection string ,(localdb)\\MSSQLLocalDB -> Name of the local sql server,  OdeToFood -> Name of the Database , Credentials -> last part

2.How does this Connection string reach my DbContext[OdeToFood.Data/OdeToFoodDbContext]   ?
A : From Configure Services : Inside of Configure Services, The Ef Provides certain methods we can use to describe the Dbcontext the App is going to use
Method : AddDbContextPool<>  -> This will pool your dbcontext in an attempt to reuse  DbContext that have been created while the App is alive which leads to better performance
and scalability (giving the class name of dbcontext "OdeToFoodDbContext"(which allows to take OdeToFoodDbContext as constructor paramter like IRestaurantData))
and it is going to need some options ,which will customize the options for this dbcontext[What i wanted to do is invoke a method which is "Usesqlserver"(This is the method 
which is installing my database provider)I am using Microsoft SQL Server  ]and now required parameter to use sql server is the connection string "options.UseSqlServer(Configuration.GetConnectionString())" 
asking configuration which has been populated with info from appsetting.json and other configuration sources and ask it to get connection string 
and place the name of connection string .now,We are saying we want to use sqlserver and with this connection string 
                    ////options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));  -> This didnot work so had to update it to   "                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"), b => b.MigrationsAssembly("OdeToFood"));"
and Here, We've configured our application to talk to my localdb but there is one more code to add 
in OdeToFoodDbContext class we need to provide enoguh code so that the framework can pass in connectionstring and other options that this dbcontext needs to know  
about to work with the database and we can do that by adding a constructor to dbcontext.and have this constructor take an object of type "(DbContextOptions<OdeToFoodDbContext> options) :base(options)" 
forwarding those options to base class constructor and that constructor can figure out what to do with all the information inside of this object.


Summary : So Now we have OdeToFoodDbContext Class that can take connection options and provider options.I have services configured in [stratup.cs]so that i can ask for an 
odetofooddbcontext inside of any component now we just need to make a step having a workable database using entity framework migrations. 
 */

