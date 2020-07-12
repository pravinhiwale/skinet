using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        /*
        Just hover the mouse to StoreContext and press cmd + .  and it help you generating constructor
        below is the constructor that we need so that we can provide it with some options and the options we'll give it is as is the connection string and then it passes those options
        up into the base constructor and the base constructor is is what we were just looking at in terms of constuctor you see in metadata of DBContext
        So we're going to pass those options up to its parent class.

        One Esstential thing we will be doing is we'll tell the DBcontext options what type we're passing up here as well 
        and will pass in the StoreContext

        */
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands {get; set;}
        public DbSet<ProductType> ProductTypes { get; set; }
        /*
        And this is going to be the plurals name of the entity and it doesn't have to be its convention that we're looking at here 
        but this "products" is gonna be the name of the table that gets created when we generate the code to generate our database
        
        */
        protected  override void  OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //decimal is not suppored in SQLlite so converting it to double
            if(Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes()){
                    var properties = entityType.ClrType.GetProperties().Where(p=>p.PropertyType ==typeof(decimal));
                    foreach (var property in properties){
                            modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }

            }

        }
    }
}

/*
dotnet --info gives you dotnet version info , it should match the Microsoft.EntityFrameworkCore

for first time use we need to add nuget package 
Microsoft.EntityFrameworkCore  (version 3.1.5 matching with dotnet core) 

for SQL lite
add Nuget package  Microsoft.EntityFrameworkCore.sqllite
Do not Add  .sqllite.core

*/

/*
if you hover mouse over DBContext it will say
A DbContext instance represents a session with the database and can be used to query and save instances of your entities. DbContext is a combination of the Unit Of Work and Repository patterns.

what this really means is that we're abstracting our database away from our code.
We we do not directly query our database.
We use our DBcontext methods to query our database.

Now if we go in right click on DBContext and go to definition then we can see the metadata about the DB context class

And what we're going to need to do for this particular class is we can add some DBContextOptions to this as well 
nd this allows us to give it a connection string so that it can go ahead and query the correct database
*/