using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TeaShop.Data;
using TeaShop.Models;
using TeaShop.Services;

namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ImageServices _imageServices;
        public SanPhamController(MyDbContext myDbContext, ImageServices imageServices)
        {
            _context = myDbContext;
            _imageServices = imageServices;
        }
        // GET: SanPham
        public IActionResult Index()
        {
            var sanPhams = _context.SanPhams.ToList();
            ViewBag.dm = _context.DanhMucs.ToList();
            return View(sanPhams);
        }
        [HttpPost]
        public async Task<IActionResult> ThemSp(SanPham sanPham,IFormFile HinhFile)
        {
            var link = await _imageServices.SaveImg(HinhFile,"images");
            if (!string.IsNullOrEmpty(link)) 
            {
                sanPham.Hinh = Path.GetFileName(link);
            }
            _context.SanPhams.Add(sanPham);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/SanPham/Index");
        }
        [HttpPost]
        public IActionResult CapNhatSP(SanPham sanPham) 
        {
            var sp = _context.SanPhams.FirstOrDefault(r=>r.MaSp == sanPham.MaSp);
            if(sp != null)
            {
                sp.TenSp = sanPham.TenSp;
                sp.MoTa = sanPham.MoTa;
                sp.SoLuong = sp.SoLuong;
                _context.SaveChanges();
            }
            return Redirect("/Admin/SanPham/Index");
        }
        public IActionResult XoaSP(string MaSp)
        {
            var sp = _context.SanPhams.FirstOrDefault(r => r.MaSp == MaSp);
            _context.SanPhams.Remove(sp);
            _context.SaveChanges();
            return Redirect("/Admin/SanPham/Index");
        }
    }
}
