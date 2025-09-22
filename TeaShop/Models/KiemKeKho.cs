using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Models
{
    [Table("KiemKeKho")]
    public class KiemKeKho
    {
        [Key]
        [StringLength(30)]
        public string MaKiemKe { get; set; }

        public DateTime NgayKiemKe { get; set; }

        [StringLength(255)]
        public string NguoiKiemKe { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public virtual ICollection<ChiTietKiemKe> ChiTietKiemKes { get; set; }
    }

}
