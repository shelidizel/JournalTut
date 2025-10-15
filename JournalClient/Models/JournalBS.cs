using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalClient.Models
{
    public class JournalBS
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PoHeader")]
        public int PoHeaderId { get; set; }

       
        public virtual Journal PoHeader { get; private set; }

        [Required]
        [ForeignKey("Product")]
        public string ProductCode { get; set; }

        public virtual Product Product { get; private set; }

        [Required]
        [ForeignKey("Unit")]
        public int UnitId { get; set; }

        public virtual Unit Unit { get; private set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Product quantity should be greater than 0 and lesser than 1000")]
        public decimal Quantity { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Amount should be greater than 0 and lesser than 1000")]
        public decimal Amount { get; set; }



        [Range(1, 1000, ErrorMessage = "Product quantity should be greater than 0 and lesser than 1000")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "smallmoney")]
        [Required]
        public decimal Fob { get; set; }

        [Range(1, 1000, ErrorMessage = "Product quantity should be greater than 0 and lesser than 1000")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "smallmoney")]
        [Required]
        public decimal PrcInBaseCurr { get; set; }

        [MaxLength(75)]
        [NotMapped]
        public string Description { get; set; } = "";

        [MaxLength(25)]
        [NotMapped]
        public string UnitName { get; set; } = "";

        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
