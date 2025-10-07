using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalClient.Models
{
    public class Product
    {
        [Remote("IsProductCodeValid", "Product", AdditionalFields = "Name", ErrorMessage = "Product Code Exists Already")]
        [Key]
        [StringLength(6)]
        public string Code { get; set; }

        [Remote("IsProductNameValid", "Product", AdditionalFields = "Code", ErrorMessage = "Product Name Exists Already")]
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Cost { get; set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Units")]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public virtual Unit Units { get; set; }



        [ForeignKey("Brands")]
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public virtual Brand Brands { get; set; }


        [ForeignKey("Categories")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public virtual Category Categories { get; set; }


        public string PhotoUrl { get; set; } = "noimage.png";

        [Display(Name = "Product Photo")]
        [NotMapped]
        public IFormFile ProductPhoto { get; set; }

        [NotMapped]
        public string BreifPhotoName { get; set; }
    }
}
