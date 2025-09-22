using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaShop.Services;
using System.Threading.Tasks;
using TeaShop.Data;
using TeaShop.Models;
using System.Text.Json;
using TeaShop.Areas.Admin.Models;
namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ImageServices _imageservices;
        public HomeController(MyDbContext context,ImageServices imageServices)
        {
            _context = context;
            _imageservices = imageServices;
        }
        public IActionResult Index()
        {
            var top4 = _context.LuotXemSanPhams.
                               OrderByDescending(x => x.LuotXem).
                               Take(4).
                               Select(x => new LuotXemSanPhamViewModel
                               {
                                   MaSp = x.MaSp,
                                   LuotXem =  x.LuotXem,
                               }).ToList();
            var top4js = JsonSerializer.Serialize(top4);
            var tonkhothap = _context.SanPhams.
                             OrderBy(x => x.SoLuong).
                             Take(5).
                             Select(x => new SanPham
                             {
                                 TenSp = x.TenSp,
                                 SoLuong = x.SoLuong,
                             });
            var tonkhothapjs = JsonSerializer.Serialize(tonkhothap);
            var doanhthu = _context.DoanhThus.
                            OrderByDescending(x => x.Thang).
                            Take(5).
                            Select(x => new DoanhThu
                            {
                                DoanhThus = x.DoanhThus,
                                Thang = x.Thang,
                            });
            var doanhthujs = JsonSerializer.Serialize(doanhthu);
            var soluongchechlech = _context.
                ChiTietKiemKes.
                Select(x => new ChiTietKiemKe
                {
                    MaSp = x.MaSp,
                    ChenhLech = x.ChenhLech,
                });
            var soluongchenhlechjson = JsonSerializer.Serialize(soluongchechlech);
            var top4spnhap = _context.ChiTietPhieuNhaps.
                OrderBy(x => x.SoLuongNhap).
                Take(4).
                Select(x => new ChiTietPhieuNhap
                {
                    MaSp = x.MaSp,
                    SoLuongNhap = x.SoLuongNhap,
                });
            var top4spnhapjson = JsonSerializer.Serialize(top4spnhap);
            ViewBag.Top4 = top4js;
            ViewBag.TonKhoThap = tonkhothapjs;
            ViewBag.DoanhThu = doanhthujs;
            ViewBag.soluongchenhlech =  soluongchenhlechjson;
            ViewBag.top4spnhap = top4spnhapjson;
            return View();
        }
        
        
        public IActionResult DeleteDh(string MaDonHang)
        {
            var result = _context.DonHangs.Where(r => r.MaDonHang == MaDonHang).FirstOrDefault();
            _context.DonHangs.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCTDH(int ID)
        {
            var result = _context.ChiTietDonHangs.Where(r => r.ID == ID).FirstOrDefault();
            _context.ChiTietDonHangs.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public IActionResult DeleteLSTT(string MaLichSuThanhToan)
        {
            var result = _context.LichSuThanhToans.Where(r => r.MaLichSuThanhToan == MaLichSuThanhToan).FirstOrDefault();
            _context.LichSuThanhToans.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
