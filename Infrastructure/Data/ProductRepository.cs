using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                        .Include(p=>p.ProductType)
                        .Include(p=>p.ProductBrand)
                        //.FindAsync(id);
                        .FirstOrDefaultAsync(p=>p.Id == id);

                    /*
                    we get an error because the find method doesn't accept IQueryable so the options are to use FirstOrDefaultAsync or SingleOrDefaultAsync
                    The only real difference between the two is that the FirstorDefault will return an entity as soon as it finds it in the list 
                    whereas singleordefault if it finds more than one of the same entity in the list and it's going to throw an exception 
                    the firstordefault won't for an exception if there's more than one in the list for our purposes right now either one of these is fine.
                    */
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
            .Include(p=>p.ProductType)
            .Include(p=>p.ProductBrand)
            .ToListAsync();

            /* 
            Include is used to add navigation properties , here we are doing eager loading , 
            we can also do lazy loading but then it will always load navigation properties which a user might not like


            */
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }


    }
    /*
     because we want this to be injectable into other classes specifically our controllers , we need to go and add this as a service in our startup class.

    */
    /*
    oour Repository repository is going to be interacting with our storecontext and then our controllers are going to use the repository methods in order to
    retrieve the data from our database.
    So our repository is abstracting our data access methods away from the controller and we discussed this earlier but this makes our controllers a bit thinner easier to manage.
    And when it comes to testing then it's much easier to test against a repository or a mock of a repository than it is to test against the storecontext.

we need to inject storeContext and hence create a constructor and inject storecontext
    */
}