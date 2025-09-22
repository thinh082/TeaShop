using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        [StringLength(30)]
        public string MaSp { get; set; }

        [StringLength(255)]
        public string TenSp { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gia { get; set; }

        public int SoLuong { get; set; }

        [StringLength(30)]
        public string MaDanhMuc { get; set; }

        [StringLength(300)]
        public string MoTa { get; set; }

        public int DaBan { get; set; }

        [StringLength(300)]
        public string Hinh { get; set; }

        [ForeignKey("MaDanhMuc")]
        public virtual DanhMuc DanhMuc { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual ICollection<ChiTietKiemKe> ChiTietKiemKes { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual ICollection<ChiTietVoHang> ChiTietVoHangs { get; set; }
        public virtual ICollection<LuotXemSanPham> LuotXemSanPhams { get; set; }
    }
}
