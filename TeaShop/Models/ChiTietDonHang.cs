using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("ChiTietDonHang")]
    public class ChiTietDonHang
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        [Column("MaDonHang",TypeName ="varchar30")]
        public string MaDonHang { get; set; }

        [StringLength(30)]
        public string MaSp { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Gia { get; set; }

        [ForeignKey("MaDonHang")]
        public virtual DonHang DonHang { get; set; }

        [ForeignKey("MaSp")]
        public virtual SanPham SanPham { get; set; }
    }

}
