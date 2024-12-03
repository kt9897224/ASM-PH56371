using Asm_ph56371.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Asm_ph56371.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AsmDbContext _context;
       

        public HomeController(ILogger<HomeController> logger, AsmDbContext db)
        {
            _logger = logger;
            _context = db;
        }

        [Authorize]
        public IActionResult Index(int page=1, int pageSize = 3)
        {
            var totalProducts = _context.SanPhams.Count();
            var products = _context.SanPhams
            .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            return View(products);
        }
        public IActionResult AddToCart(Guid productId)
        {
            var product = _context.SanPhams.Find(productId);
            if (product != null)
            {
                string IMGURL = product.IMGURL;
                // Lấy giỏ hàng từ Session
                string cartData = HttpContext.Session.GetString("Cart");
                List<SanPham> cart = string.IsNullOrEmpty(cartData)
                    ? new List<SanPham>()
                    : System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(cartData);

                // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                var existingProduct = cart.FirstOrDefault(p => p.SPId == productId);
                if (existingProduct != null)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng
                    existingProduct.SoLuongTonKho++; // Tăng số lượng
                }
                else
                {
                    // Nếu chưa có, thêm sản phẩm mới vào giỏ hàng với số lượng mặc định là 1
                    product.SoLuongTonKho = 1;
                    cart.Add(product);
                }

                // Lưu giỏ hàng vào Session
                HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cart));

                TempData["Message"] = $"{product.TenSanPham} đã được thêm vào giỏ hàng.";
            }

            // Chuyển đến trang Cart
            return RedirectToAction("Cart", "CTGH");
        }




        [HttpPost]
        public IActionResult UpdateCart(Guid productId, string action)
        {
            // Lấy giỏ hàng từ Session
            string cartData = HttpContext.Session.GetString("Cart");
            List<SanPham> cart = string.IsNullOrEmpty(cartData)
                ? new List<SanPham>()
                : System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(cartData);

            // Tìm sản phẩm trong giỏ hàng
            var product = cart.FirstOrDefault(p => p.SPId == productId);

            if (product != null)
            {
                if (action == "increase")
                {
                    // Tăng số lượng nếu nhấn nút "+", kiểm tra số lượng tối đa nếu có
                    product.SoLuongTonKho++;
                }
                else if (action == "decrease")
                {
                    // Giảm số lượng nếu nhấn nút "-", nếu số lượng > 1
                    if (product.SoLuongTonKho > 1)
                    {
                        product.SoLuongTonKho--;
                    }
                    else
                    {
                        // Nếu số lượng 1, xóa sản phẩm khỏi giỏ hàng
                        cart.Remove(product);
                    }
                }
            }

            // Lưu lại giỏ hàng vào Session
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cart));

            return RedirectToAction("Cart","CTGH");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();  // Trả về view AccessDenied
        }
    }
}
