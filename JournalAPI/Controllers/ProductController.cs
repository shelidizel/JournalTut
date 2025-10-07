using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public ProductController(JournalDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        // GET: api/<ProductController>
        [HttpGet("~/api/product")]
        public ActionResult<List<Product>> Get()
        {
            List<Product> products = _dbContext.Products.ToList();
            if (products == null)
            {
                return NotFound();
            }

            return products; ;
        }

        // GET api/<ProductController>/5
        [HttpGet("~/api/product/{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _dbContext.Products.Find(id);
            if ( product == null) return NotFound("Product not found");
            return product;
        }

        // POST api/<ProductController>
        [HttpPost("~/api/product")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            if (product == null) return BadRequest("product data not found");

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                product
             );
        }

        // PUT api/<ProductController>/5
        [HttpPut("~/api/product/{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            _dbContext.Attach(product);
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Put),
                product
                );
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("~/api/product/{id}")]
        public ActionResult Delete(int id, [FromBody] Product product)
        {
            _dbContext.Attach(product);
            _dbContext.Entry(product).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Delete),
                product
                );
        }
    }
}
