using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Respository
{
    public class CheckOutRespository
    {
        private readonly MyDbContext _context;
        private readonly string _connectionstring;
        public CheckOutRespository(MyDbContext myDbContext, IConfiguration configuration)
        {
            _context = myDbContext;
            _connectionstring = configuration.GetConnectionString("MyConnectionString");
        }
        public async Task<string> ChuyenGioHangSangDonHangAsync(string email, TaiKhoan taiKhoan, decimal tongTien,string HinhThucThanhToan)
        {
            var voHang = await _context.VoHangs.FirstOrDefaultAsync(v => v.Email == email);
            if (voHang == null) return null;

            var chiTietGio = await _context.ChiTietVoHangs
                                           .Where(c => c.MaVoHang == voHang.MaVoHang)
                                           .ToListAsync();
            if (!chiTietGio.Any()) return null;

            var maDonHang = Guid.NewGuid().ToString().Substring(0, 10);
            var donHang = new DonHang
            {
                MaDonHang = maDonHang,
                Email = email,
                DiaChiGiaoHang = taiKhoan.DiaChi,
                TrangThai = "Chờ xác nhận",
                NgayDat = DateTime.Now,
                NgayGiao = DateTime.Now.AddDays(2),
                TongTien = tongTien
            };

            _context.DonHangs.Add(donHang);

            foreach (var item in chiTietGio)
            {
                var sanPham = await _context.SanPhams.FindAsync(item.MaSp);
                if (sanPham != null)
                {
                    _context.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        MaDonHang = maDonHang,
                        MaSp = item.MaSp,
                        SoLuong = item.SoLuong,
                        Gia = sanPham.Gia
                    });
                    sanPham.SoLuong = sanPham.SoLuong - item.SoLuong;
                }
            }
            var maLichSu = Guid.NewGuid().ToString().Substring(0, 15);
            var lichSuThanhToan = new LichSuThanhToan
            {
                MaLichSuThanhToan = maLichSu,
                Email = email,
                HinhThucThanhToan = HinhThucThanhToan , // hoặc truyền từ input nếu có
                SoTienThanhToan = tongTien,
                NgayGiaoDich = DateTime.Now,
                MaDonHang = maDonHang,
                TrangThai = "Chờ Xác Nhận"
            };
            var sanpham = 
            _context.LichSuThanhToans.Add(lichSuThanhToan);

            _context.ChiTietVoHangs.RemoveRange(chiTietGio);

            await _context.SaveChangesAsync();
            return maDonHang;
        }
        public void ChuyenGioHangSangDonHang(SqlParameter[] sqlParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("ChuyenGioHangSangDonHang", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (sqlParameters != null && sqlParameters.Length > 0)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
