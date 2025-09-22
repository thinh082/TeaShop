using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace TeaShop.Models
{
    [Table("DoanhThu")]
    public class DoanhThu
    {
        [Key]
        public int Id { get; set; }

        [Column("DoanhThu",TypeName = "money")]
        public decimal? DoanhThus { get; set; }

        [StringLength(15)]
        public string Thang { get; set; }
    }
}
