using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
