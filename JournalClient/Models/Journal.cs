using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace JournalClient.Models
{
    public class Journal
    {
        [Key]
        public int JournalID {  get; set; }

        [Required]
        [MaxLength(50)]
        public string JournalNumber { get; set; }

        public DateTime JournalDate {  get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

     
        public Supplier Supplier { get; set; }


        [Required]
        public int BaseCurrencyId { get; set; }

        
        public virtual Currency Currency { get; private set; }

        [Required]
        public int PoCurrencyId { get; set; }

      
        public virtual Currency POCurrency { get; private set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        [Range(0, 1000000, ErrorMessage = "Should be greater than 0")]
        public decimal ExchangeRate { get; set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal DiscountPercentage { get; set; }

        [Required]
        [MaxLength(100)]
        public string QuotationNumber { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime QuotationDate { get; set; } = DateTime.Now.Date;

        [Required]
        [MaxLength(500)]
        public string PaymentTerms { get; set; }

        [Required]
        [MaxLength(500)]
        public string Remarks { get; set; }

        public virtual List<JournalBS> JournalBSs { get; set; } = new List<JournalBS>();

        public virtual List<JournalPL> JournalPLs { get; set; } = new List<JournalPL>();
    }
}
