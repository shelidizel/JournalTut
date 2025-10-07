
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JournalAPI.DTO
    {
        /// <summary>
        /// DTO for creating a single Balance Sheet (BS) line item.
        /// Excludes database-generated keys and navigation properties.
        /// </summary>
        public class JournalBSCreateDto
        {
            [Required(ErrorMessage = "Product Code is required for the BS line item.")]
            public string ProductCode { get; set; }

            [Required(ErrorMessage = "Quantity is required.")]
            [Range(1, 1000, ErrorMessage = "Product quantity should be greater than 0 and lesser than 1000.")]
            public decimal Quantity { get; set; }

            [Required(ErrorMessage = "FOB value is required.")]
            [Range(1, 1000, ErrorMessage = "FOB value should be greater than 0 and lesser than 1000.")]
            public decimal Fob { get; set; }

            [Required(ErrorMessage = "Price in Base Currency is required.")]
            [Range(1, 1000, ErrorMessage = "Price in Base Currency should be greater than 0 and lesser than 1000.")]
            public decimal PrcInBaseCurr { get; set; }
        }

        /// <summary>
        /// DTO for creating a single Profit & Loss (PL) line item.
        /// Excludes database-generated keys and navigation properties.
        /// </summary>
        public class JournalPLCreateDto
        {
            [Required(ErrorMessage = "Account ID is required for the PL line item.")]
            public int AccountId { get; set; }

            [Required(ErrorMessage = "Description is required.")]
            [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Unit ID is required.")]
            public int UnitID { get; set; }

            [Required(ErrorMessage = "Start Date is required.")]
            [DataType(DataType.DateTime)]
            public DateTime StartDate { get; set; }

            [Required(ErrorMessage = "IsStart flag is required.")]
            public bool Isstart { get; set; }

            [Required(ErrorMessage = "Amount is required.")]
            [Range(1, 1000, ErrorMessage = "Amount should be greater than 0 and lesser than 1000.")]
            public int Amount { get; set; }
        }

        /// <summary>
        /// The main, complex nested DTO for creating a complete Journal record (Header + Lines).
        /// </summary>
        public class JournalCreateNestedDto
        {
            // === HEADER FIELDS (from Journal model) ===

            [Required(ErrorMessage = "Journal number is required.")]
            [StringLength(50, ErrorMessage = "Journal number cannot exceed 50 characters.")]
            public string JournalNumber { get; set; }

            public DateTime? JournalDate { get; set; }

            [Required(ErrorMessage = "Supplier ID is required.")]
            public int SupplierID { get; set; }

            [Required(ErrorMessage = "Base Currency ID is required.")]
            public int BaseCurrencyId { get; set; }

            [Required(ErrorMessage = "PO Currency ID is required.")]
            public int PoCurrencyId { get; set; }

            [Required(ErrorMessage = "Exchange Rate is required.")]
            [Range(1, 1000000, ErrorMessage = "Exchange Rate should be greater than 0 and lesser than 1,000,000.")]
            public decimal ExchangeRate { get; set; }

            [Required(ErrorMessage = "Discount Percentage is required.")]
            public decimal DiscountPercentage { get; set; }

            [Required(ErrorMessage = "Quotation Number is required.")]
            [StringLength(100, ErrorMessage = "Quotation number cannot exceed 100 characters.")]
            public string QuotationNumber { get; set; }

            [Required(ErrorMessage = "Quotation Date is required.")]
            [DataType(DataType.DateTime)]
            public DateTime QuotationDate { get; set; }

            [Required(ErrorMessage = "Payment Terms are required.")]
            [StringLength(500, ErrorMessage = "Payment Terms cannot exceed 500 characters.")]
            public string PaymentTerms { get; set; }

            [Required(ErrorMessage = "Remarks are required.")]
            [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters.")]
            public string Remarks { get; set; }

            // === NESTED COLLECTION FIELDS (Line Items) ===

            [Required(ErrorMessage = "Journal Balance Sheet lines are required.")]
            public List<JournalBSCreateDto> JournalBSs { get; set; } = new List<JournalBSCreateDto>();

            [Required(ErrorMessage = "Journal Profit & Loss lines are required.")]
            public List<JournalPLCreateDto> JournalPLs { get; set; } = new List<JournalPLCreateDto>();
        }
    }


