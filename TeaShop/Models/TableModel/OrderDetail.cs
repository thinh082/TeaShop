namespace TeaShop.Models.TableModel
{
    public class OrderDetail
    {
        public string MaDonHang { get; set; }
        public string Email { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public decimal TongTien { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string MaSp { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
    }

}
