using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(255)]
        public string Password { get; set; }
       
    }
}
