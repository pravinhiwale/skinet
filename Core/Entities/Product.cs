
namespace Core.Entities
{
    public class Product:BaseEntity
    {
        
        public string Name  { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
       
        public int ProductTypeId { get; set; }
         //what we've created here are related entitites and the way that we've configured this all the way that we've added these properties in here to give 
        // it the full type as well as the Id is to help our entity framework so that when it creates or when we create a new migration it's going to know 
        //that the product has a relationship with both the product type and the product band and it's going to use this information to set up those relationships 
        //for us as well as the foreign keys.
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }  

/*
    in our product we have bunch of properties  

*/
    }
}

/*
We don't query our database directly in asp.net core.
We typically go through or via an Object Relational mapper that abstracts the database away from our code 
and this comes with several advantages it's certainly one of the advantages is it allows us to very 
easily swap the database provided that we're using
And like I say we're using sql lite for development but we won't be using simple sqllite in production

We'll swap it for a production database server such as sql or mysql but because we're going to
use an Object Relational map this makes it very easy for us to do so and the Object Relational map that
we're going to use is entity framework the one that Microsoft creates and obviously works very well with dot net core




*/

/*
OK so now that we've configured our database context (storeContext.cs) what we can look at now is entity framework core migrations
And this is going to generate some code so that we can scaffold our database and create our database based on the code that we've written so far.

And what this is going to do it's going to take a look in our StoreContext.
It's going to read from this  public DbSet<Product> Products {get;set;}.
It's going to see that we've got a DB set property here related to our product entity and it's going
to generate a table called "Products" based on the name that we specified here, 
this table's going to contain two columns based on the properties inside our product class
So we can expect to have an "Id" column as well as a "Name" column 
and by convention an entity Framework Core is convention based.
If we specify an "Id" that is an integer and it's called "Id" that it's going to use this field as the primary key of the table
And because it's an integer it's going to set the column up to auto generates a new id every time 
a new record or a new product is added to that database and without us needing to do any further configuration 
this is just what it's going to do by convention

for dot net migration tool use sdk version rather the host version
check using dotnet --info

i used below

dotnet tool install --global dotnet-ef --version  3.1.1
dotnet tool install --global dotnet-ef --version  3.1.1

dotnet ef migrations add InitialCreate -o Data/Migrations

after running above you will get error 
Your startup project 'API' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.

you just need to add package  Microsoft.EntiryFrameworkCore.Design 

after that update database
dotnet ef database update

*/