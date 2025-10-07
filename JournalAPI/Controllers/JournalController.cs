using JournalAPI.Data;
using JournalAPI.DTO;
using JournalAPI.Model;
using Microsoft.AspNetCore.Mvc;
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
            List<Journal> journals = _dbContext.Journals.ToList();
            return journals;
        }

        // GET api/<JournalController>/5
        [HttpGet("~/api/Journal/{id}")]
        public ActionResult<Journal> Get(int id)
        {
            var journal = _dbContext.Journals.Find(id);

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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JournalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
