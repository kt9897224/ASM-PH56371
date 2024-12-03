using Asm_ph56371.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace Asm_ph56371.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly AsmDbContext _context;

        public NguoiDungController(AsmDbContext context)
        {
            _context = context;
        }

        // GET: NguoiDung/DangKy
        public IActionResult DangKy()
        {
            return View();
        }

        // POST: NguoiDung/DangKy
        [HttpPost]
        public IActionResult DangKy(string email, string matKhau, int vaiTro)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu email đã tồn tại
                var existingUser = _context.NguoiDung.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    ViewBag.Error = "Email đã tồn tại!";
                    return View();
                }

                var newUser = new NguoiDung
                {
                    Email = email,
                    MatKhau = matKhau,
                    VaiTro = vaiTro
                };

                _context.NguoiDung.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("DangNhap");
            }
            return View();
        }

        // GET: NguoiDung/DangNhap
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: NguoiDung/DangNhap
        [HttpPost]
        public IActionResult DangNhap(string email, string matKhau)
        {
            var user = _context.NguoiDung.FirstOrDefault(u => u.Email == email && u.MatKhau == matKhau);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.VaiTro == 1 ? "Admin" : "Khach") // Vai trò Admin hoặc Khách
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Đăng nhập người dùng
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("index", "home"); // Redirect to Home or another page based on role
            }
            else
            {
                ViewBag.Error = "Email hoặc mật khẩu không đúng!";
                return View();
            }
        }

        // GET: NguoiDung/Logout
        public IActionResult Logout()
        {
            // Đăng xuất người dùng
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("DangNhap");
        }

        // Quản lý người dùng (Chỉ Admin có quyền truy cập)
        [Authorize(Roles = "Admin")]
        public IActionResult QuanLyNguoiDung()
        {
            var users = _context.NguoiDung.ToList();
            return View(users);
        }


        
    }
}
