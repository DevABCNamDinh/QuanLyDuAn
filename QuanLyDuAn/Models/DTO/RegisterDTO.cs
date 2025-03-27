using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(255)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("Password",ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]

        [MaxLength(255)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]

        [MaxLength(20)]
        public string Phone { get; set; }
    }
}
