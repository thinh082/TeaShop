using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhieuNhapController : Controller
    {
        private readonly MyDbContext _context;
        public PhieuNhapController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public IActionResult Index()
        {
            var pn = _context.PhieuNhaps.ToList();
            return View(pn);
        }
        [HttpPost]
        public IActionResult ThemPN(PhieuNhap phieuNhap)
        {
            _context.PhieuNhaps.Add(phieuNhap);
            _context.SaveChanges();
            return Redirect("/Admin/PhieuNhap/Index");
        }
        [HttpPost]
        public IActionResult CapNhatPN(PhieuNhap phieuNhap)
        {
            var pn = _context.PhieuNhaps.FirstOrDefault(r => r.MaPhieuNhap == phieuNhap.MaPhieuNhap);
            if (pn != null)
            {
                pn.NguoiNhap = phieuNhap.NguoiNhap;
                pn.NgayNhap = phieuNhap.NgayNhap;
                _context.SaveChanges();
            }
            return Redirect("/Admin/PhieuNhap/Index");
        }
        public IActionResult XoaPN(string MaPhieuNhap)
        {
            var pn = _context.PhieuNhaps.FirstOrDefault(r => r.MaPhieuNhap == MaPhieuNhap);
            _context.PhieuNhaps.Remove(pn);
            _context.SaveChanges();
            return Redirect("/Admin/PhieuNhap/Index");
        }
    }
}
