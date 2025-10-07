using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public CategoryController(JournalDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        // GET: api/<CategoryController>
        [HttpGet("~/api/category")]
        public ActionResult<List<Category>> Get()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // GET api/<CategoryController>/5
        [HttpGet("~/api/category/{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound("Category not found");
            return category;
        }

        // POST api/<CategoryController>
        [HttpPost("~/api/category")]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (category == null) return BadRequest("Category data not found");

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = category.Id },
                category
             );
        }

        // PUT api/<CategoryController>/5
        [HttpPut("~/api/category/{id}")]
        public ActionResult<Category> Put(int id, [FromBody] Category category)
        {
            _dbContext.Attach(category);
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Put),
                category
                );
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("~/api/category/{id}")]
        public ActionResult Delete(int id, [FromBody] Category category)
        {
            _dbContext.Attach(category);
            _dbContext.Entry(category).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Delete),
                category
                );
        }
    }
}
