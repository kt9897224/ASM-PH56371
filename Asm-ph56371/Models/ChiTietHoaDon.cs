using System.ComponentModel.DataAnnotations;

namespace Asm_ph56371.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public Guid CTHDId { get; set; } // Khóa chính GUID

        [Required]
        public Guid? HoaDonId { get; set; } // Khóa ngoại đến HóaĐơn

        [Required]
        public Guid? SanPhamId { get; set; } // Khóa ngoại đến SảnPhẩm

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; } // Số lượng sản phẩm

        [Required]
        [DataType(DataType.Currency)]
        public decimal Gia { get; set; } // Giá tại thời điểm mua

        // Quan hệ với HóaĐơn
        public HoaDon? HoaDon { get; set; }

        // Quan hệ với SảnPhẩm
        public SanPham? SanPham { get; set; }
    
    }
}
