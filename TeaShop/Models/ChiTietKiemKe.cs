using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("ChiTietKiemKe")]
    public class ChiTietKiemKe
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string MaKiemKe { get; set; }

        [StringLength(30)]
        public string MaSp { get; set; }

        public int? SoLuongHeThong { get; set; }

        public int? SoLuongThucTe { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? ChenhLech { get; set; }

        [StringLength(255)]
        public string? LyDo { get; set; }

        [ForeignKey("MaKiemKe")]
        public virtual KiemKeKho KiemKeKho { get; set; }

        [ForeignKey("MaSp")]
        public virtual SanPham SanPham { get; set; }
    }
}
