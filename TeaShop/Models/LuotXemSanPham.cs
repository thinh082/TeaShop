using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShop.Models
{
    [Table("LuotXemSanPham")]
    public class LuotXemSanPham
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string MaSp { get; set; }

        public int LuotXem { get; set; }

        public DateTime? ThoiGian { get; set; }

        [ForeignKey("MaSp")]
        public virtual SanPham SanPham { get; set; }
    }
}
