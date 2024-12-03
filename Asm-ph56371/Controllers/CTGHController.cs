using Asm_ph56371.Models;
using Microsoft.AspNetCore.Mvc;

public class CTGHController : Controller
{
    private readonly AsmDbContext _context;

    public CTGHController(AsmDbContext context)
    {
        _context = context;
    }

    public IActionResult Cart()
    {
        // Lấy giỏ hàng từ Session
        string cartData = HttpContext.Session.GetString("Cart");

        // Debug: In ra dữ liệu giỏ hàng lưu trong session
        Console.WriteLine($"Giỏ hàng trong session: {cartData}");

        List<SanPham> cart = string.IsNullOrEmpty(cartData)
            ? new List<SanPham>()
            : System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(cartData);

      

        // Tính tổng tiền sản phẩm
        decimal totalAmount = cart.Sum(item => item.Gia.HasValue ? item.Gia.Value * item.SoLuongTonKho : 0);

        // Phí ship cố định
        decimal shippingFee = 15.00m;

        // Tổng tiền thanh toán (tổng tiền sản phẩm + phí vận chuyển)
        decimal totalPrice = totalAmount + shippingFee;

        // Truyền giỏ hàng vào ViewBag để hiển thị
        ViewBag.Cart = cart;
        ViewBag.TotalAmount = totalAmount;
        ViewBag.ShippingFee = shippingFee;
        ViewBag.TotalPrice = totalPrice;

        return View(cart);
    }

    [HttpPost]
    public IActionResult ThanhToan()
    {
        // Lấy giỏ hàng từ Session
        string cartData = HttpContext.Session.GetString("Cart");

        // Nếu giỏ hàng trống, thông báo lỗi và quay lại trang giỏ hàng
        if (string.IsNullOrEmpty(cartData))
        {
            TempData["Error"] = "Giỏ hàng của bạn hiện tại trống!";
            return RedirectToAction("Cart");
        }

        // Deserialize giỏ hàng từ Session
        List<SanPham> cart = System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(cartData);

        // Kiểm tra giỏ hàng có sản phẩm
        if (cart == null || !cart.Any())
        {
            TempData["Error"] = "Giỏ hàng của bạn hiện tại trống!";
            return RedirectToAction("Cart");
        }

        // Tính tổng tiền sản phẩm trong giỏ hàng
        decimal totalAmount = cart.Sum(item => item.Gia.HasValue ? item.Gia.Value * item.SoLuongTonKho : 0);

        // Phí ship cố định
        decimal shippingFee = 15.00m;

        // Tổng tiền thanh toán
        decimal totalPrice = totalAmount + shippingFee;

        // Lấy người dùng hiện tại từ Claims
        string emailNguoiDung = User.Identity.Name;
        var nguoiDung = _context.NguoiDung.FirstOrDefault(u => u.Email == emailNguoiDung);

        if (nguoiDung == null)
        {
            TempData["Error"] = "Không tìm thấy thông tin người dùng!";
            return RedirectToAction("Cart");
        }

        // Tạo hóa đơn mới
        HoaDon hoaDon = new HoaDon
        {
            hoaDonId = Guid.NewGuid(),
            NguoiDungId = nguoiDung.nguoiDungId,
            TongTien = totalPrice,
            NgayTao = DateTime.Now,
            ChiTietHoaDons = new List<ChiTietHoaDon>()
        };

        // Thêm chi tiết hóa đơn
        foreach (var item in cart)
        {
            hoaDon.ChiTietHoaDons.Add(new ChiTietHoaDon
            {
                CTHDId = Guid.NewGuid(),
                HoaDonId = hoaDon.hoaDonId,
                SanPhamId = item.SPId,
                SoLuong = item.SoLuongTonKho,
                Gia = item.Gia ?? 0
            });
        }

        // Lưu hóa đơn vào cơ sở dữ liệu
        _context.HoaDons.Add(hoaDon);
        _context.SaveChanges();

        // Xóa giỏ hàng sau khi thanh toán
        HttpContext.Session.Remove("Cart");

        // Thông báo thanh toán thành công
        TempData["Message"] = $"Thanh toán thành công! Tổng cộng: {totalPrice:C} (Đã bao gồm phí vận chuyển: {shippingFee:C})";

        // Chuyển hướng đến trang danh sách hóa đơn dành cho admin
        return RedirectToAction("ThankYou", "CTGH");
    }
     


    public IActionResult ThankYou()
    {
        return View(); // Trả về view cảm ơn
    }

    public IActionResult Xoa(Guid productId)
    {
        // Lấy giỏ hàng từ Session
        string cartData = HttpContext.Session.GetString("Cart");
        List<SanPham> cart = string.IsNullOrEmpty(cartData)
            ? new List<SanPham>()
            : System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(cartData);

        // Tìm và xóa sản phẩm khỏi giỏ hàng
        var productToRemove = cart.FirstOrDefault(p => p.SPId == productId);
        if (productToRemove != null)
        {
            cart.Remove(productToRemove);
        }

        // Lưu lại giỏ hàng vào Session
        HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cart));

        TempData["Message"] = "Sản phẩm đã được xóa khỏi giỏ hàng.";
        return RedirectToAction("Cart");
    }

    // Thêm sản phẩm vào giỏ hàng
    public IActionResult ThemVaoCart(Guid productId)
    {
        // Lấy sản phẩm từ cơ sở dữ liệu
        var product = _context.SanPhams.Find(productId);
        if (product == null)
        {
            TempData["Error"] = "Sản phẩm không tồn tại!";
            return RedirectToAction("Index", "SanPham");
        }

        // Lấy giỏ hàng từ Session
        string cartData = HttpContext.Session.GetString("Cart");
        List<SanPham> cart = string.IsNullOrEmpty(cartData)
            ? new List<SanPham>()
            : System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(cartData);

        // Thêm sản phẩm vào giỏ hàng
        cart.Add(product);

        // Lưu lại giỏ hàng vào Session
        HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cart));

        TempData["Message"] = "Sản phẩm đã được thêm vào giỏ hàng.";
        return RedirectToAction("Cart");
    }
}
