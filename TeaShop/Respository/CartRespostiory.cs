using Microsoft.EntityFrameworkCore;
using System.Linq;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Respository
{
    public class CartRespostory
    {
        private readonly MyDbContext _context;
        public CartRespostory(MyDbContext context)
        {
            _context = context;
        }
        public async Task ThemSanPhamVaoGioAsync(string email, string maSp,int SoLuong)
        {
            var voHang = await _context.VoHangs.FirstOrDefaultAsync(v => v.Email == email);           
            var chiTiet = await _context.ChiTietVoHangs.FirstOrDefaultAsync(c => c.MaVoHang == voHang.MaVoHang && c.MaSp == maSp);
            if (chiTiet != null)
            {
                chiTiet.SoLuong += SoLuong;
                await _context.SaveChangesAsync(); 
            }
            else
            {
                _context.ChiTietVoHangs.Add(new ChiTietVoHang
                {
                    MaVoHang = voHang.MaVoHang,
                    MaSp = maSp,
                    SoLuong = SoLuong
                });
                await _context.SaveChangesAsync();
            }
        }
        public int SumProduct(string Email)
        {
            return _context.VoHangs.Where(r => r.Email == Email).SelectMany(r => r.ChiTietVoHangs).Count();
        }
        public Decimal ToTalPrice(string Email)
        {
            var voHang =  _context.VoHangs.FirstOrDefault(v => v.Email == Email);
            var chitietVH = _context.ChiTietVoHangs.Where(c=>c.MaVoHang == voHang.MaVoHang).ToList();
            decimal tongtien = 0;
            foreach (var chitiet in chitietVH) 
            {
                var sanpham = _context.SanPhams.FirstOrDefault(r=>r.MaSp == chitiet.MaSp);
                if (sanpham != null) 
                {
                    tongtien += sanpham.Gia * chitiet.SoLuong;
                }
            }
            return tongtien;
        }
        public async Task<List<SanPham>> XemGioHangAsync(string email)
        {
            var danhSach = await (from v in _context.VoHangs
                                  join ct in _context.ChiTietVoHangs on v.MaVoHang equals ct.MaVoHang
                                  join sp in _context.SanPhams on ct.MaSp equals sp.MaSp
                                  where v.Email == email
                                  select new
                                  {
                                      SanPham = sp,
                                      SoLuong = ct.SoLuong // Lấy số lượng sản phẩm trong giỏ hàng từ ChiTietVoHang
                                  }).ToListAsync();

            return danhSach.Select(x => new SanPham
            {
                MaSp = x.SanPham.MaSp,
                TenSp = x.SanPham.TenSp,
                MoTa = x.SanPham.MoTa,
                Gia = x.SanPham.Gia,
                SoLuong = x.SoLuong // Cập nhật số lượng cho từng sản phẩm
            }).ToList();
        }

        public async Task XoaSanPhamKhoiGio(string email, string maSp)
        {
            var voHang = await _context.VoHangs.FirstOrDefaultAsync(v => v.Email == email);
            if (voHang != null)
            {
                var chiTiet = await _context.ChiTietVoHangs.FirstOrDefaultAsync(c => c.MaVoHang == voHang.MaVoHang && c.MaSp == maSp);
                if (chiTiet != null)
                {
                    _context.ChiTietVoHangs.Remove(chiTiet);
                    await _context.SaveChangesAsync();
                }
            }
        }  
        public async Task<bool> CapNhatSoLuongSanPhamAsync(string email, string maSp, int soLuongMoi)
        {
            var voHang = await _context.VoHangs.FirstOrDefaultAsync(v => v.Email == email);
            if (voHang == null) return false;

            var chiTiet = await _context.ChiTietVoHangs.FirstOrDefaultAsync(c => c.MaVoHang == voHang.MaVoHang && c.MaSp == maSp);
            if (chiTiet == null) return false;

            if (soLuongMoi <= 0)
            {
                _context.ChiTietVoHangs.Remove(chiTiet);
            }
            else
            {
                chiTiet.SoLuong = soLuongMoi;
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}

