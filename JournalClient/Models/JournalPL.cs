using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalClient.Models
{
    public class JournalPL
    {

        [Key]
        public int JournalPID { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

     
        public virtual Account Account { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Units")]
        public int UnitID { get; set; }

  
        public virtual Unit Units { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        [Required]
        public bool Isstart { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Product quantity should be greater than 0 and lesser than 1000")]
        public int Amount { get; set; }
    }
}
