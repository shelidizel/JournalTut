using System.ComponentModel.DataAnnotations;

namespace JournalAPI.Model
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
        public string Descrition { get; set; }
    }
}
