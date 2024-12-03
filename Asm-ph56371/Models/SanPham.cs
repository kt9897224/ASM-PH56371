using System.ComponentModel.DataAnnotations;

namespace Asm_ph56371.Models
{
    public class SanPham
    {
        [Key]
        public Guid SPId { get; set; }  // Khóa chính GUID

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
        public decimal? Gia { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        public string MoTa { get; set; }

        public string? IMGURL { get; set; } // Lưu đường dẫn ảnh

        public int SoLuongTonKho { get; set; } // Số lượng tồn kho

        public ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
        public ICollection<ChiTietHoaDon>? chiTietHoaDons { get; set; }
    }
}
