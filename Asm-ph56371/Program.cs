using Asm_ph56371.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ cho DbContext
builder.Services.AddDbContext<AsmDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm dịch vụ xác thực với Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/NguoiDung/DangNhap"; // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/NguoiDung/Logout"; // Đường dẫn đến trang đăng xuất
        options.AccessDeniedPath = "/Home/AccessDenied";  // Trang lỗi khi không có quyền
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie hết hạn sau 30 phút
        options.SlidingExpiration = true;
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Thêm dịch vụ phân quyền
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("Khach"));
});

// Thêm MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRouting();
// Cấu hình middleware cho ứng dụng
app.UseAuthentication(); // Đảm bảo middleware authentication được chạy
app.UseAuthorization();  // Đảm bảo middleware authorization được chạy
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NguoiDung}/{action=DangNhap}/{id?}");

app.Run();
