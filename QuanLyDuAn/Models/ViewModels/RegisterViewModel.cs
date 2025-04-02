using System.ComponentModel.DataAnnotations;

namespace QuanLyDuAn.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
