using Microsoft.AspNetCore.Mvc;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly MyDbContext _context;
        public DonHangController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        public IActionResult Index()
        {
            var donhang = _context.DonHangs.ToList();
            return View(donhang);
        }
        [HttpPost]
        public IActionResult ThemDH(DonHang donHang)
        {
            _context.DonHangs.Add(donHang);
            _context.SaveChanges();
            return Redirect("/Admin/DonHang/Index");
        }
        [HttpPost]
        public IActionResult CapNhatDH(DonHang donHang)
        {
            var dh = _context.DonHangs.FirstOrDefault(r => r.MaDonHang == donHang.MaDonHang);
            if (dh != null)
            {
                dh.NgayDat = donHang.NgayDat;
                dh.NgayGiao = donHang.NgayGiao;
                dh.NgayDat = dh.NgayDat;
                dh.Email = donHang.Email;
                dh.TrangThai = donHang.TrangThai;
                dh.TongTien = donHang.TongTien;
                _context.SaveChanges();
            }
            return Redirect("/Admin/DonHang/Index");
        }
        public IActionResult XoaDH(string MaDonHang)
        {
            var dh = _context.DonHangs.FirstOrDefault(r => r.MaDonHang == MaDonHang);
            _context.DonHangs.Remove(dh);
            _context.SaveChanges();
            return Redirect("/Admin/DonHang/Index");
        }

    }
}
