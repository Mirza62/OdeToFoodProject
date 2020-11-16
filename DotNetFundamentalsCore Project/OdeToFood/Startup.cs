using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"), b => b.MigrationsAssembly("OdeToFood"));
                ////options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            /*This is the method .net core will invoke to say tell me all the components and 
            services that you need E.g: IRestaurantData what should implement this interface
            Singleton means i want a single instance of a specific service for the entire application.
            when ever someone needs IRestaurantData please provide the component IMemoryRestaurantData

            services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();


            We use to say use in memory restaurantdata when ever you need an IRestaurantData but now i want to use sqlrestaurantdata
            i aslo make change addsingleton to addscoped  .A singleton DbContext or a singleton Restaurant data would be very bad
            for the application typically the way you want to use your data access components id you're using the EF behind the scenes 
            is to have your services scoped to a particular http request so,every time the framework hands out a sqlrestaurant data.
            that going to keep handing out the same instance of sqlrestaurantdata for a single request when the next request comes in
            that might be a new restaurantData but this allows the DbContext that's working behind the scenes to collect all the changes that are
            needed during a single request so,I now have services.AddScoped.and using a sqlrestaurantdata implementation.*/
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddRazorPages();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(SayHelloMiddleWare);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        private RequestDelegate SayHelloMiddleWare(RequestDelegate next)
        {

            return async ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("HelloWorld");
                }

                else
                {
                    await next(ctx);
                }

            };
        }
    }
}
