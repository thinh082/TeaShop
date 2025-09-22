using Microsoft.EntityFrameworkCore;
using TeaShop.Data;
using TeaShop.Models;
using TeaShop.Services;
using Microsoft.AspNetCore.Identity;
namespace TeaShop.Responsitory
{
    public class UserRespository
    {
        private readonly MyDbContext _context;
        private readonly PasswordServices _passwordSerivecs;
        public UserRespository(MyDbContext myDbContext,PasswordServices passwordServices)
        {
            _context = myDbContext;
            _passwordSerivecs = passwordServices;
        }
        public void DangKy(TaiKhoan taiKhoanMoi)
        {
            string hasps = _passwordSerivecs.HashPass(taiKhoanMoi.MatKhau);
            taiKhoanMoi.MatKhau = hasps;
            taiKhoanMoi.Role_id = false;
            _context.TaiKhoans.Add(taiKhoanMoi);

            // Tạo vỏ hàng mặc định cho tài khoản
            var voHangMoi = new VoHang
            {
                MaVoHang = Guid.NewGuid().ToString().Substring(0,8),
                Email = taiKhoanMoi.Email
            };
            _context.VoHangs.Add(voHangMoi);

            _context.SaveChanges();
        }
        public bool DangNhap(string email, string matKhau,out TaiKhoan taiKhoan, out string mess)
        {
            mess = "";
            taiKhoan = null;
            TaiKhoan? tk = _context.TaiKhoans.FirstOrDefault(r => r.Email == email);

            if (tk == null)
            {
                mess = "Email không tồn tại.";
                return false;
            }

            var check = _passwordSerivecs.HashPasswordVerification(tk.MatKhau, matKhau);

            if (check == PasswordVerificationResult.Failed)
            {
                mess = "Mật khẩu không đúng.";
                return false;
            }
            taiKhoan = tk;
            return true;
        }
        public TaiKhoan ThongTinNguoiDung(string Email)
        {
            return _context.TaiKhoans.FirstOrDefault(r => r.Email == Email);
        }
        public void CapNhatThongTinNguoiDung(TaiKhoan taiKhoan)
        {
            TaiKhoan us = _context.TaiKhoans.FirstOrDefault(r=>r.Email == taiKhoan.Email);
            if(us != null)
            {
                us.Ten = taiKhoan.Ten;
                us.Ho = taiKhoan.Ho;
                us.DiaChi =taiKhoan.DiaChi;
                us.SoDienThoai = taiKhoan.SoDienThoai;
                _context.SaveChanges();
            }
        }

    }
}
