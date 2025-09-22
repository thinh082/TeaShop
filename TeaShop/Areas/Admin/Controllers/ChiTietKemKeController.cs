using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietKemKeController : Controller
    {
        private readonly MyDbContext _context;
        public ChiTietKemKeController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public IActionResult Index()
        {
            var danhSach =  _context.ChiTietKiemKes.ToList();
            return View(danhSach);
        }
        // POST: thêm mới
        [HttpPost]
        public async Task<IActionResult> ThemCTKK(ChiTietKiemKe model)
        {
            try
            {
                _context.ChiTietKiemKes.Add(model);
                await _context.SaveChangesAsync();
                return Redirect("/Admin/ChiTietKemKe/Index");
            }
            catch (Exception ex) { return RedirectToAction("Error"); }
        }

        // POST: cập nhật
        [HttpPost]
        public async Task<IActionResult> CapNhatCTKK(ChiTietKiemKe model)
        {
            try
            {
                var entity = await _context.ChiTietKiemKes.FindAsync(model.ID);
                if (entity != null)
                {
                    entity.MaKiemKe = model.MaKiemKe;
                    entity.MaSp = model.MaSp;
                    entity.SoLuongHeThong = model.SoLuongHeThong;
                    entity.SoLuongThucTe = model.SoLuongThucTe;
                    entity.LyDo = model.LyDo;

                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }
                return Redirect("/Admin/ChiTietKemKe/Index");
            }
            catch (Exception ex) { return RedirectToAction("Error"); }
        }

        // GET: xóa
        public async Task<IActionResult> XoaCTKK(int ID)
        {
            var entity = await _context.ChiTietKiemKes.FindAsync(ID);
            if (entity != null)
            {
                _context.ChiTietKiemKes.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return Redirect("/Admin/ChiTietKemKe/Index");
        }

    }
}
