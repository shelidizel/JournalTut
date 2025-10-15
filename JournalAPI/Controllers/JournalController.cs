using JournalAPI.Data;
using JournalAPI.DTO;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly JournalDBContext _dbContext;

        public JournalController(JournalDBContext context)
        { 
            _dbContext = context;
        }

        // GET: api/<JournalController>
        [HttpGet("~/api/journal")]
        public ActionResult<List<Journal>> Get()
        {
            List<Journal> journals = _dbContext.Journals
                .Include(s => s.Supplier)
                .Include(c => c.Currency)
                .Include(c => c.POCurrency)
                .ToList();
            return journals;
        }

        // GET api/<JournalController>/5
        [HttpGet("~/api/Journal/{id}")]
        public ActionResult<Journal> Get(int id)
        {
            var journal = _dbContext.Journals
                .Include(s => s.Supplier)
                .Include(c => c.Currency)
                .Include(c => c.POCurrency)
                .Include(c => c.JournalBSs)
                .Include(c => c.JournalPLs)
                .FirstOrDefault(j => j.JournalID == id);

            if (journal == null) 
            {
                return NotFound();
            }

            return journal;

        }

        // POST api/<JournalController>
        [HttpPost("~/api/journal")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Journal))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Journal>> AddJournal([FromBody] JournalCreateNestedDto journalDTO)
        {

            List<JournalPL> journalPLs = new List<JournalPL>();

            foreach (JournalPLCreateDto item in journalDTO.JournalPLs) {

                JournalPL journalPL = new JournalPL { 
                    AccountId = item.AccountId,
                    Amount = item.Amount,
                    Description = item.Description,
                    UnitID = item.UnitID,
                    StartDate = item.StartDate,
                    Isstart = item.Isstart,
                };

                journalPLs.Add(journalPL);
            }

            List<JournalBS> journalBSs = new List<JournalBS>();

            foreach (JournalBSCreateDto item in journalDTO.JournalBSs)
            {

                JournalBS journalBS = new JournalBS
                {
                    ProductCode = item.ProductCode,
                    Quantity = item.Quantity,
                    Fob = item.Fob,
                    PrcInBaseCurr = item.PrcInBaseCurr,
                    Amount = item.Amount,
                    UnitId = (int)item.UnitId
                };

                journalBSs.Add(journalBS);
            }

            Journal journal = new Journal
            {
                JournalNumber = journalDTO.JournalNumber,
                JournalDate = (DateTime)journalDTO.JournalDate,
                SupplierID = journalDTO.SupplierID,
                BaseCurrencyId = journalDTO.BaseCurrencyId,
                PoCurrencyId = journalDTO.PoCurrencyId,
                ExchangeRate = journalDTO.ExchangeRate,
                DiscountPercentage = journalDTO.DiscountPercentage,
                QuotationNumber = journalDTO.QuotationNumber,
                QuotationDate = journalDTO.QuotationDate,
                PaymentTerms = journalDTO.PaymentTerms,
                Remarks = journalDTO.Remarks,
                JournalBSs = journalBSs,
                JournalPLs = journalPLs,
            };

            _dbContext.Journals.Add(journal);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = journal.JournalID },
                journal
             );
        }

        // PUT api/<JournalController>/5
        [HttpPut("~/api/Journal/{id}")]
        public ActionResult<Journal> Put(int id, [FromBody] JournalCreateNestedDto journalDTO)
        {
           
            Journal journal = _dbContext.Journals
                .Include(j => j.JournalPLs)
                .Include(j => j.JournalBSs)
                .FirstOrDefault(j => j.JournalID == id);

            if (journal == null) return NotFound();


            journal.JournalBSs.Clear();
            journal.JournalPLs.Clear();

          
            journal.JournalNumber = journalDTO.JournalNumber;
            journal.JournalDate = (DateTime)journalDTO.JournalDate;
            journal.SupplierID = journalDTO.SupplierID;
            journal.BaseCurrencyId = journalDTO.BaseCurrencyId;
            journal.PoCurrencyId = journalDTO.PoCurrencyId;
            journal.ExchangeRate = journalDTO.ExchangeRate;
            journal.DiscountPercentage = journalDTO.DiscountPercentage;
            journal.QuotationNumber = journalDTO.QuotationNumber;
            journal.QuotationDate = journalDTO.QuotationDate;
            journal.PaymentTerms = journalDTO.PaymentTerms;
            journal.Remarks = journalDTO.Remarks;

            // 4. Create NEW child entities
            List<JournalPL> journalPLs = journalDTO.JournalPLs.Select(item => new JournalPL
            {
                AccountId = item.AccountId,
                Amount = item.Amount,
                Description = item.Description,
                UnitID = item.UnitID,
                StartDate = item.StartDate,
                Isstart = item.Isstart
            }).ToList();

            List<JournalBS> journalBSs = journalDTO.JournalBSs.Select(item => new JournalBS
            {
                ProductCode = item.ProductCode,
                Quantity = item.Quantity,
                Fob = item.Fob,
                PrcInBaseCurr = item.PrcInBaseCurr,
                Amount = (int)item.Amount,
                UnitId = (int)item.UnitId
            }).ToList();

            journal.JournalPLs = journalPLs;
            journal.JournalBSs = journalBSs;

            _dbContext.SaveChanges();

            return Ok(journal); 
        }

        // DELETE api/<JournalController>/5
        [HttpDelete("~/api/journal/{id}")]
        public ActionResult<Journal> Delete(int id)
        {
            
            Journal? journal = _dbContext.Journals.Find( id );
            if (journal == null) return NotFound();
            Debug.WriteLine("======================");
            Debug.WriteLine(journal.JournalID);
            _dbContext.Journals.Attach(journal);
            _dbContext.Journals.Remove(journal);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(Delete),
                new { id = journal.JournalID },
                journal
             );
        }



    }
}
