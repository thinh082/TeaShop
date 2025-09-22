using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("ChiTietPhieuNhap")]
    public class ChiTietPhieuNhap
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string MaPhieuNhap { get; set; }

        [StringLength(30)]
        public string MaSp { get; set; }

        public int? SoLuongNhap { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? GiaNhap { get; set; }

        [ForeignKey("MaPhieuNhap")]
        public PhieuNhap PhieuNhap { get; set; }

        [ForeignKey("MaSp")]
        public SanPham SanPham { get; set; }
    }
}
