using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public CurrencyController(JournalDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        // GET: api/<CurrencyController>
        [HttpGet("~/api/currency")]
        public ActionResult<List<Currency>> Get()
        {
            List<Currency> currencies = _dbContext.Currencies.ToList();
            if (currencies == null)
            {
                return NotFound();
            }

            return currencies;
        }

        // GET api/<CurrencyController>/5
        [HttpGet("~/api/currency/{id}")]
        public ActionResult<Currency> Get(int id)
        {
            var currency = _dbContext.Currencies.Find(id);
            if (currency == null) return NotFound("Category not found");
            return currency;
        }

        // POST api/<CurrencyController>
        [HttpPost("~/api/currency")]
        public async Task<ActionResult<Currency>> AddCurrency([FromBody] Currency currency)
        {
            if (currency == null) return BadRequest("Currency data not found");

            _dbContext.Currencies.Add(currency);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = currency.Id },
                currency
             );
        }

        // PUT api/<CurrencyController>/5
        [HttpPut("~/api/currency/{id}")]
        public ActionResult<Currency> Put(int id, [FromBody] Currency currency)
        {
            _dbContext.Attach(currency);
            _dbContext.Entry(currency).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Put),
                currency
                );
        }

        // DELETE api/<CurrencyController>/5
        [HttpDelete("~/api/currency/{id}")]
        public ActionResult Delete(int id, [FromBody] Currency currency)
        {
            _dbContext.Attach(currency);
            _dbContext.Entry(currency).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return CreatedAtAction(
                nameof(Delete),
                currency
                );
        }
    }
}
