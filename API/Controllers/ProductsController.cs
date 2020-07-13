using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")] removing since we are derviing our class from baseapiController which already has it
    public class ProductsController : BaseApiController
    {
        // private readonly StoreContext _context;
        //we will replace the storeContext with the instance of our repository hence commenting above and replcace StoreContext with IProductRepository
        //private readonly IProductRepository _repo;
        // public ProductsController(IProductRepository repo)
        // {
        //     _repo = repo;
        //    // _context = context;
        // }
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        //using Generics controller
        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo
        , IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;

            //adding so much dependencies is not right , we will tackle this later


        }
        /* in Below function because  we're sending up our parameters as a query string but we've told our API controller that these are and objects now (productspecParams) 
        hen this is going to start to look at the body of the request and of course we don't have a body 
        when we're using an httpget request and this is confusing our API controller ,it's not able to automatically bind these productparameters to method here.
        So what we need to do is we need to tell our API to go and look for these properties in the query string and we can do that by specifying the fromquery attributes here.*/
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            //commenting below line to use pagination 
        //public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(
            //string sort,int? brandId,int? typeId)
            //since we neeed more params for paging , created a separate class for the params
            [FromQuery]ProductSpecParams productParams)
          {
            //var products = await _context.Products.ToListAsync();
            //var products = await _repo.GetProductsAsync();

            //using generics
            // var products = await _productsRepo.ListAllAsync();
            // return Ok(products);

            //using generics with specifications
           // var spec = new ProductsWithTypesAndBrandsSpecification(sort,brandId,typeId); (commented because we created a separate class for params and we need to use that)
           var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
           var countSpec = new ProductWithFiltersForCountSpecification(productParams);
           var totalItems  = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
           
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems, data));


        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        /*
        Now this is just an example of how we can return and tell swagger about the correct type of responses.
        */

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // return await _context.Products.FindAsync(id);
            //  we can still just use the return In this case we didn't have to specify "Ok" because if we return a product from this it's going to be 200 response Response anyway
            //return await _repo.GetProductByIdAsync(id);

            //using generic repository
            // return Ok(await _productsRepo.GetByIdAsync(id));
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product= await _productsRepo.GetEntityWithSpec(spec);
            if (product ==null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            // return Ok(await _repo.GetProductBrandsAsync());

            //using Generic Repository
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //return Ok(await _repo.GetProductTypesAsync());
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }

    //we will replace the storeContext with the instance of our repository
}