using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public SupplierController(JournalDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        // GET: api/<SupplierController>
        [HttpGet("~/api/supplier")]
        public ActionResult<List<Supplier>> Get()
        {
            List<Supplier> suppliers = _dbContext.Suppliers.ToList();
            if (suppliers == null)
            {
                return NotFound();
            }

            return suppliers;
        }

        // GET api/<SupplierController>/5
        [HttpGet("~/api/supplier/{id}")]
        public ActionResult<Supplier> Get(int id)
        {
            var supplier = _dbContext.Suppliers.Find(id);
            if (supplier == null) return NotFound("Supplier not found");
            return supplier;
        }

        // POST api/<SupplierController>
        [HttpPost("~/api/supplier")]
        public async Task<ActionResult<Supplier>> AddSupplier([FromBody] Supplier supplier)
        {
            if (supplier == null) return BadRequest("Supplier data not found");

            _dbContext.Suppliers.Add(supplier);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                supplier
             );
        }

        // PUT api/<SupplierController>/5
        [HttpPut("~/api/supplier/{id}")]
        public ActionResult<Supplier> Put(int id, [FromBody] Supplier supplier)
        {
            _dbContext.Attach(supplier);
            _dbContext.Entry(supplier).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Put),
                supplier
                );
        }

        // DELETE api/<SupplierController>/5
        [HttpDelete("~/api/supplier/{id}")]
        public ActionResult Delete(int id, [FromBody] Supplier supplier)
        {
            _dbContext.Attach(supplier);
            _dbContext.Entry(supplier).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Delete),
                supplier
                );
        }
    }
}
