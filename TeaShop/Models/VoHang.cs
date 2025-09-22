using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("VoHang")]
    public class VoHang
    {
        [Key]
        [StringLength(30)]
        public string MaVoHang { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [ForeignKey("Email")]
        public TaiKhoan TaiKhoan { get; set; }

        public virtual ICollection<ChiTietVoHang> ChiTietVoHangs { get; set; }
    }
}
