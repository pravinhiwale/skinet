using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            //var products = await _context.Products.ToListAsync();
            //var products = await _repo.GetProductsAsync();

            //using generics
            // var products = await _productsRepo.ListAllAsync();
            // return Ok(products);

            //using generics with specifications
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // return await _context.Products.FindAsync(id);
            //  we can still just use the return In this case we didn't have to specify "Ok" because if we return a product from this it's going to be 200 response Response anyway
            //return await _repo.GetProductByIdAsync(id);

            //using generic repository
            // return Ok(await _productsRepo.GetByIdAsync(id));
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product= await _productsRepo.GetEntityWithSpec(spec);
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