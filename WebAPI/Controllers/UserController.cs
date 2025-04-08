using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuAn.Models;
using QuanLyDuAn.Models.ViewModels;
using QuanLyDuAn.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService,IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null || string.IsNullOrEmpty(loginViewModel.Username) || string.IsNullOrEmpty(loginViewModel.Password))
            {
                return BadRequest(new { message = "Vui lòng nhập đầy đủ Username và Password." });
            }

            var user = _userService.Login(loginViewModel.Username, loginViewModel.Password);
            if (user != null)
            {
                var token = _jwtService.GenerateToken(loginViewModel.Username, "Admin");

                // Lưu thông tin vào Session
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("AuthToken", token);

                return Ok(new
                {
                    message = "Đăng nhập thành công!",
                    userId = user.Id,
                    username = user.Username,
                    token = token
                });
            }

            return Unauthorized(new { message = "Username hoặc Password không chính xác!" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerViewModel.Username,
                Password = registerViewModel.Password // Cần mã hóa mật khẩu trước khi lưu vào DB
            };

            var registeredUser = _userService.Register(user);
            return Ok(new
            {
                message = "Đăng ký thành công!",
                userId = registeredUser.Id,
                username = registeredUser.Username
            });
        }
    }
}
