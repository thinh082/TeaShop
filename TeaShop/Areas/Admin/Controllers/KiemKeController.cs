using Microsoft.AspNetCore.Mvc;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KiemKeController : Controller
    {
        private readonly MyDbContext _context;
        public KiemKeController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public IActionResult Index()
        {
            var kk = _context.KiemKeKhos.ToList();
            return View(kk);
        }
        [HttpPost]
        public IActionResult ThemKiemKe(KiemKeKho kiemKeKho)
        {
            _context.KiemKeKhos.Add(kiemKeKho);
            _context.SaveChanges();
            return Redirect("/Admin/KiemKe/Index");
        }
        [HttpPost]
        public IActionResult CapNhatKiemKe(KiemKeKho kiemKeKho)
        {
            var kk = _context.KiemKeKhos.FirstOrDefault(r=>r.MaKiemKe == kiemKeKho.MaKiemKe);   
            if(kk != null)
            {
                kk.NguoiKiemKe = kiemKeKho.NguoiKiemKe;
                kk.NgayKiemKe = kiemKeKho.NgayKiemKe;
                kk.GhiChu = kiemKeKho.GhiChu;
                _context.SaveChanges();
            }
            return Redirect("/Admin/KiemKe/Index");
        }
        public IActionResult XoaKiemKe(string MaKiemKe)
        {
            var kk = _context.KiemKeKhos.FirstOrDefault(r => r.MaKiemKe == MaKiemKe);
            _context.KiemKeKhos.Remove(kk);
            _context.SaveChanges();
            return Redirect("/Admin/KiemKe/Index");
        }
    }
}
