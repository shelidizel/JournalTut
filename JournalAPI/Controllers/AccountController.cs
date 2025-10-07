using JournalAPI.Data;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public AccountController(JournalDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        // GET: api/<AccountController>
        [HttpGet("~/api/account")]
        public ActionResult<List<Account>> Get()
        {
            List<Account> accounts = _dbContext.Accounts.ToList();
            if (accounts == null)
            {
                return NotFound();
            }

            return accounts;
        }

        // GET api/<AccountController>/5
        [HttpGet("~/api/account/{id}")]
        public ActionResult<Account> Get(int id)
        {
            var account = _dbContext.Accounts.Find(id);
            if (account == null) return NotFound("Account not found");
            return account;
        }

        // POST api/<AccountController>
        [HttpPost("~/api/account")]
        public async Task<ActionResult<Account>> AddAccount([FromBody] Account account)
        {
            if (account == null) return BadRequest("Account data not found");

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                account
             );
        }

        // PUT api/<AccountController>/5
        [HttpPut("~/api/account/{id}")]
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

        // DELETE api/<AccountController>/5
        [HttpDelete("~/api/account/{id}")]
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
