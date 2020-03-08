using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ResponseFilterExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public ProductController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // with filter
        [HttpGet("w/{id}")]
        [ResponseFilterMode(FilterMode.Exclude)]
        [ResponseFilter("Supplier")]
        public async Task<IActionResult> Get(long id)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);

            // do your business with product

            // if you do not want to return fields just add response filter

            return Ok(product);
        }

        // without filter
        // if I need to filter supplier field, I would need to create another model (mapping)
        [HttpGet("wo1/{id}")]
        public async Task<IActionResult> GetWithoutFilter1(long id)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);

            // do your business with product

            // if you do not want to return fields use mapper

            ProductResponseModel response = product.Adapt<ProductResponseModel>();

            return Ok(response);
        }

        // without filter
        // if I need to filter another field I would need another model
        // lets say that this time I want to filter both store and supplier
        [HttpGet("wo2/{id}")]
        public async Task<IActionResult> GetWithoutFilter2(long id)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);

            // do your business with product

            // if you do not want to return fields use mapper

            ProductResponseModelAnother response = product.Adapt<ProductResponseModelAnother>();

            return Ok(response);
        }
    }
}
