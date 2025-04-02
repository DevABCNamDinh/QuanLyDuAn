using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Author { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        public string ImagePath { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
