using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDuAn.AppDBContext;
using QuanLyDuAn.Models;
using QuanLyDuAn.Models.DTO;

namespace QuanLyDuAn.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDBContext _context;

        public UsersController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,FullName,Phone,IsActive,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,FullName,Phone,IsActive,CreatedAt,UpdatedAt")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UpdatedAt = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.Id == id);
        }
       
        public async Task<IActionResult> Login()
        {
            var user = new LoginDTO();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                if (user!=null)
                {
                    var userLogin = await _context.users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
                    if (userLogin != null)
                    {
                        HttpContext.Session.SetString("UserId", userLogin.Id.ToString());
                        HttpContext.Session.SetString("UserName", userLogin.FullName.ToString());
                        TempData["Message"] = "Đăng nhập thành công";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "Thông tin đăng nhập sai!";

                        return View(user);
                    }
                        

                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public async Task<IActionResult> Register()
        {
            var user = new RegisterDTO();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            try
            {
                User userRegister = new User();
                if (user!=null)
                {
                    userRegister.Email = user.Email;
                    userRegister.Password = user.Password;
                    userRegister.FullName = user.FullName;
                    userRegister.Phone = user.Phone;
                    _context.users.Add(userRegister);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Đăng ký thành công";

                    return RedirectToAction("Login");
                }
                return View(user);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            TempData["Message"] = "Đăng xuất thành công";

            return RedirectToAction("Index", "Home");
        }
    }
}
