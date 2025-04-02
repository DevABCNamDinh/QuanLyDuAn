using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid CartId { get; set; }

        [Required]
        [Range(0, 100)]
        public int Quantity { get; set; }

        public Book Book { get; set; }
        public Cart Cart { get; set; }
    }
}
