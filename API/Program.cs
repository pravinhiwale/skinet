using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();  original only line
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services=scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context,loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex,"An Error occured during migration");
                }
            }
            host.Run();
            /*
            we need to do is go and get access to our datacontext but what we can do is because we're outside of our services container 
            in the startup class where we don't have control over the lifetime of when we create this particular instance of our context
             we're going to see this inside a "using" statement and a using statement means that any code that runs inside of this is going to be disposed of as soon as
             we've finished with the methods inside that we don't need to worry about cleaning up after ourselves
            and because we're not relying on asp.net core to handle the lifetime of when this is created and disposed We need to do this inside of a a using statement.

            */

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.ConfigureKestrel(serverOptions =>{
                    //     serverOptions.ConfigureHttpsDefaults(configureOptions=>{
                    //         configureOptions.ServerCertificate=
                    //     });
                    // });
                });
    }
}
