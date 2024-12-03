using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm_ph56371.Models
{
    public class GioHang
    {
        [Key]
        public Guid gioHangId { get; set; }  // Khóa chính GUID
        public string UserName { get; set; }

        [Required]
        public Guid? NguoiDungId { get; set; } // Khóa ngoại đến NgườiDùng

        // Quan hệ 1-1 với NgườiDùng
        public NguoiDung? NguoiDung { get; set; }

        // Quan hệ 1-n với ChiTietGioHang
        public ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
    }
}
