using Microsoft.AspNetCore.Mvc;
using Asm_ph56371.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

public class HoaDonController : Controller
{
    private readonly AsmDbContext _context;

    public HoaDonController(AsmDbContext context)
    {
        _context = context;
    }

    // Trang hiển thị danh sách hóa đơn
    [Authorize (Roles ="Admin")]
    public IActionResult Index()
    {
        var hoaDons = _context.HoaDons
       .Include(hd => hd.NguoiDung) // Include để lấy dữ liệu liên kết
       .OrderByDescending(hd => hd.NgayTao)
       .ToList();

        return View(hoaDons);
    }
}
