using Microsoft.AspNetCore.Mvc;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietPhieuNhapController : Controller
    {
        private readonly MyDbContext _context;
        public ChiTietPhieuNhapController(MyDbContext myDbContext)
        {
           _context = myDbContext;            
        }
        public IActionResult Index()
        {
            var ctpn = _context.ChiTietPhieuNhaps.ToList();
            return View(ctpn);
        }
        [HttpPost]
        public IActionResult ThemCTPN(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            _context.ChiTietPhieuNhaps.Add(chiTietPhieuNhap);
            _context.SaveChanges();
            return Redirect("/Admin/ChiTietPhieuNhap/Index");
        }
        [HttpPost]
        public IActionResult CapNhatPN(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            var ctpn = _context.ChiTietPhieuNhaps.Where(r => r.ID == chiTietPhieuNhap.ID).FirstOrDefault();
            if(ctpn != null)
            {
                ctpn.MaPhieuNhap = chiTietPhieuNhap.MaPhieuNhap;
                ctpn.GiaNhap = chiTietPhieuNhap.GiaNhap;
                ctpn.SoLuongNhap = chiTietPhieuNhap.SoLuongNhap;
                _context.SaveChanges();
            }
            return Redirect("/Admin/ChiTietPhieuNhap/Index");
        }
        public IActionResult XoaCTPN(int ID)
        {
            var ctpn = _context.ChiTietPhieuNhaps.Where(r => r.ID == ID).FirstOrDefault();
            _context.ChiTietPhieuNhaps.Remove(ctpn);
            _context.SaveChanges();
            return Redirect("/Admin/ChiTietPhieuNhap/Index");
        }

    }
}
