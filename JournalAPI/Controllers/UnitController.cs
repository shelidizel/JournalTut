using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public UnitController(JournalDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        // GET: api/<UnitController>
        [HttpGet("~/api/unit")]
        public ActionResult<List<Unit>> Get()
        {
            List<Unit> units = _dbContext.Units.ToList();
            if (units == null)
            {
                return NotFound();
            }

            return units;
        }

        // GET api/<UnitController>/5
        [HttpGet("~/api/unit/{id}")]
        public ActionResult<Unit> Get(int id)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null) return NotFound("Unit not found");
            return unit;
        }

        // POST api/<UnitController>
        [HttpPost("~/api/unit")]
        public async Task<ActionResult<Unit>> AddCategory([FromBody] Unit unit)
        {
            if (unit == null) return BadRequest("Unit data not found");

            _dbContext.Units.Add(unit);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = unit.Id },
                unit
             );
        }

        // PUT api/<UnitController>/5
        [HttpPut("~/api/unit/{id}")]
        public ActionResult<Unit> Put(int id, [FromBody] Unit unit)
        {
            _dbContext.Attach(unit);
            _dbContext.Entry(unit).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Put),
                unit
                );
        }

        // DELETE api/<UnitController>/5
        [HttpDelete("~/api/unit/{id}")]
        public ActionResult Delete(int id, [FromBody] Unit unit)
        {
            _dbContext.Attach(unit);
            _dbContext.Entry(unit).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Delete),
                unit
                );
        }
    }
}
