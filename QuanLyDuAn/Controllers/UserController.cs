using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuAn.Models;
using   QuanLyDuAn.Models.ViewModels;
using QuanLyDuAn.Service;

namespace QuanLyDuAn.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _userService.Login(loginViewModel.Username, loginViewModel.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Username.ToString());
                var token = _jwtService.GenerateToken(loginViewModel.Username, "Admin");

                // Lưu Token vào Session
                HttpContext.Session.SetString("AuthToken", token);
                return RedirectToAction("Index", "Book");
            }
            else
            {
                ViewBag.Error = "Username hoặc password không chính xác";
                return View(loginViewModel);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Username = registerViewModel.Username,
                    Password = registerViewModel.Password
                };
                var registeredUser = _userService.Register(user);
                return RedirectToAction("Login");
            }
            else
            {
                return View(registerViewModel);
            }
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("AuthToken");
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Book");
        }

    }
}
