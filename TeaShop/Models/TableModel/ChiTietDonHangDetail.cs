namespace TeaShop.Models.TableModel
{
    public class ChiTietDonHangDetail
    {
        public int ID { get; set; }
        public string MaDonHang { get; set; }
        public string MaSp { get; set; }
        public int? SoLuong { get; set; }
        public decimal? Gia { get; set; }
        public SanPhamDetail SanPham { get; set; }
    }

}
