using System.ComponentModel.DataAnnotations;

namespace Asm_ph56371.Models
{
    public class HoaDon
    {
        [Key]
        public Guid hoaDonId { get; set; } // Khóa chính GUID

        [Required]
        public Guid? NguoiDungId { get; set; } // Khóa ngoại đến NgườiDùng

        [Required]
        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; } // Tổng tiền hóa đơn

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime NgayTao { get; set; } // Ngày tạo hóa đơn

        // Quan hệ 1-n với NgườiDùng
        public NguoiDung? NguoiDung { get; set; }

       
        public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
