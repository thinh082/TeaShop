using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("ChiTietVoHang")]
    public class ChiTietVoHang
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string MaVoHang { get; set; }

        [StringLength(30)]
        public string MaSp { get; set; }

        public int SoLuong { get; set; }

        [ForeignKey("MaVoHang")]
        public VoHang VoHang { get; set; }

        [ForeignKey("MaSp")]
        public SanPham SanPham { get; set; }
    }
}
