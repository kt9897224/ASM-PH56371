using System.ComponentModel.DataAnnotations;

namespace Asm_ph56371.Models
{
    public class NguoiDung
    {
        [Key]
        public Guid nguoiDungId { get; set; } // Khóa chính

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 50 ký tự")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public int  VaiTro { get; set; } // "Admin" hoặc "Khach"

        // Quan hệ 1-n với HóaĐơn
        public  ICollection<HoaDon>? HoaDon { get; set; }

        // Quan hệ 1-1 với GiỏHàng
        public  GioHang? GioHang { get; set; }
    }
}
