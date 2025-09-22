using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Ho { get; set; }

        [StringLength(255)]
        public string Ten { get; set; }

        [StringLength(255)]
        public string SoDienThoai { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        public bool IsEmail { get; set; }

        public bool? Role_id { get; set; }

        [StringLength(15)]
        public string? Code { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<LichSuThanhToan> LichSuThanhToans { get; set; }
        public  virtual ICollection<VoHang> VoHangs { get; set; }
    }

}
