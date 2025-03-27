using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
