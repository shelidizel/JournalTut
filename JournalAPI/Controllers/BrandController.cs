using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public BrandController(JournalDBContext dBContext) 
        { 
            _dbContext = dBContext;
        }
        
        [HttpGet("~/api/brand")]
        public ActionResult<List<Brand>> Get()
        {
            List<Brand> brands = _dbContext.Brands.ToList();
            if(brands == null)
            {
                return NotFound();
            }

            return brands;
        }

        
        [HttpGet("~/api/brand/{id}")]
        public ActionResult<Brand> Get(int id)
        {
            var brand = _dbContext.Brands.Find(id);
            if (brand == null) return NotFound("Brand not found");
            return brand;
        }

       
        [HttpPost("~/api/brand")]
        public async Task<ActionResult<Brand>> AddBrand([FromBody] Brand brand)
        {
            if (brand == null) return BadRequest("Brand data not found");

            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = brand.Id},
                brand
             );
        }

        
        [HttpPut("~/api/brand/{id}")]
        public ActionResult<Brand> Put(int id, [FromBody] Brand brand)
        {
            _dbContext.Attach(brand);
            _dbContext.Entry(brand).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Put),
                brand
                );
        }

        
        [HttpDelete("~/api/brand/{id}")]
        public ActionResult Delete(int id, [FromBody] Brand brand)
        {
            _dbContext.Attach(brand);
            _dbContext.Entry(brand).State = EntityState.Deleted; 
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Delete),
                brand
                ); 
        }
    }
}
