using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
            //Configuration = configuration;
        }

        // public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddTransient
            /*One of them is called add transient and this one is instantiated for a individual method and not the request itself.
            So it's got a very short lifetime the repository will be created and destroyed just upon using an individual method and that's too short for what we're looking at here
          //services.AddSingleton
       
            The other option is to add a singleton and this means that if we use this option the repository would be created the first time we use it when the application starts and the method goes to the controller
            and creates a new instance of the repository but then it would never be destroyed until the application shuts down and that's too long.

            so the correct one is the addscoped option and this is the one that we'll use for almost everything.
            And you would use less typically unless you knew you had a good reason for not using this particular lifetime.
So it's gonna be created when the HTTP request comes into our API creates a new instance in the controller
the controller sees that it needs a repository so it creates the instance of the repository.
the controller sees that it needs a repository so it creates the instance of the repository.
And we don't ourselves need to worry about disposing of the resources created when a request comes in because of these life cycles.

So in order to add our repository we can say that Addscoped and we'll pass in the Irepository and that shoulbe be IProdcutRepository
and then we pass in the instance of the concrete class and that's going to be the ProductRepository
        */
        //    services.AddScoped<IProductRepository,ProductRepository>();
        //     services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        //moved above 2 lines to extensions

            services.AddAutoMapper(typeof(MappingProfiles));
            //you need to add mapper and then we just need to specify the location of where our mapping profiles are located.
            //And when we say locationi it's really about the Assembly where we've created our mapping profile class and in order to do this we can just specify typeof.
            services.AddControllers();
            /*
            after adding connectionString in AppSettings.development.json 
            we need to add our data context or our store context as a service so that we can use it in other parts of our application
            */

            /*
            ordering inside here doesn't really matter We can add our service wherever we want and it's going to be added to this Iservicecollection.
            */
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            // services.Configure<ApiBehaviorOptions> (options => {
            //     options.InvalidModelStateResponseFactory = actionContext=> {
            //         var errors = actionContext.ModelState
            //                     .Where(e =>e.Value.Errors.Count>0)
            //                     .SelectMany(x=>x.Value.Errors)
            //                     .Select(x=>x.ErrorMessage).ToArray();
            //         var errorResponse = new ApiValidationErrorResponse
            //         {
            //             Errors = errors
            //         };
            //         return new BadRequestObjectResult(errorResponse);

            //     };
            // });
            //moved above to extension
            services.AddApplicationServices();
            // services.AddSwaggerGen(c => {
            //     c.SwaggerDoc("v1",new OpenApiInfo{Title ="SkiNet Api",Version="v1"});
            // });
            //moved above to swaggerExtensions
            services.AddSwagerDocumentation();
            services.AddCors( Opt =>{
                Opt.AddPolicy("CorsPolicy",policy =>{
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            //clean up of startup class
            /*'re going to extend the IService collection class so that we can move some of our own classes or our own services.
            */
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            //Using our own middleware hence commenting above

            app.UseMiddleware<ExceptionMiddleware>();


            /*
            when it comes to adding middleware in the configure method  the ordering is very important but for services we can just add them whereever please.
            */
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            //So in the event that request comes into our API server but we don't have an end point that matches that  particular request
            // then we're going to hit this bit of middleware and it's going to redirect to our errors controller and pass in the status code 
            //and in our errors controller in this particular route


           // app.UseHttpsRedirection(); //https didn't work on my machine hence commenting

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            // app.UseSwagger();
            // app.UseSwaggerUI(c=>{c.SwaggerEndpoint("/swagger/v1/swagger.json","Skinet API v1");});
            //move above to extensions
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

/*

*/
