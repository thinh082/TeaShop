namespace TeaShop.Models.TableModel
{
    public class OrderDetailModel
    {
        public string MaDonHang { get; set; }
        public string Email { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public decimal TongTien { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public TaiKhoanDetail TaiKhoan { get; set; }
        public List<ChiTietDonHangDetail> ChiTietDonHangs { get; set; }
        public List<LichSuThanhToanDetail> lichSuThanhToans { get; set; }
    }
}
