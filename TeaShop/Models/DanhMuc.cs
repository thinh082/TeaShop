using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShop.Models
{
    [Table("DanhMuc")]
    public class DanhMuc
    {
        [Key]
        [StringLength(30)]
        public string MaDanhMuc { get; set; }

        [StringLength(255)]
        public string TenDanhMuc { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
