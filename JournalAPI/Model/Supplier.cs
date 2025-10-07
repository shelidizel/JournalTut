using System.ComponentModel.DataAnnotations;

namespace JournalAPI.Model
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        public string SupplierCode { get; set; }

        [Required]
        [MaxLength(60)]
        public string SupplierName { get; set; }

        [Required]
        public string SupplierEmail { get; set; }

        [Required]
        public string SupplierPhone { get; set; }
    }
}
