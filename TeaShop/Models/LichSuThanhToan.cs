using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("LichSuThanhToan")]
    public class LichSuThanhToan
    {
        [Key]
        [StringLength(30)]
        public string MaLichSuThanhToan { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(255)]
        public string HinhThucThanhToan { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SoTienThanhToan { get; set; }

        public DateTime? NgayGiaoDich { get; set; }

        [StringLength(30)]
        public string MaDonHang { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        [ForeignKey("Email")]
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
