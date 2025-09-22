using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
        [Table("DonHang")]
        public class DonHang
        {
            [Key]
            [Column(TypeName = "varchar(30)")]
            public string MaDonHang { get; set; }

            [Column(TypeName = "nvarchar(30)")]
            public string Email { get; set; }

            [Column(TypeName = "nvarchar(30)")]
            public string TrangThai { get; set; }

            public DateTime NgayDat { get; set; }

            public DateTime NgayGiao { get; set; }
            public decimal TongTien { get; set; }

            [Column(TypeName = "nvarchar(255)")]
            public string DiaChiGiaoHang { get; set; }

            [ForeignKey("Email")]
            public TaiKhoan TaiKhoan { get; set; }

            public virtual ICollection<LichSuThanhToan> lichSuThanhToans { get; set; }
            public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        }
}
