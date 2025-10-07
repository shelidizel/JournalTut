using System.ComponentModel.DataAnnotations;

namespace JournalClient.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        [MaxLength(100)]
        public string AccountName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
