using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeaShop.Models;
using TeaShop.Models.ParamModel;
using TeaShop.Respository;

namespace TeaShop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartRespostory cartRespostory;
        public CartController(CartRespostory cartRespostori)
        {
            cartRespostory  = cartRespostori;
        }
        public async Task<IActionResult> Index()
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                decimal tongTien = cartRespostory.ToTalPrice(email);
                ViewBag.TongTien = tongTien;
                
                List<SanPham> sp = await cartRespostory.XemGioHangAsync(email);
                return View(sp);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Lỗi khi tải giỏ hàng: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<JsonResult> ThemSP([FromBody]ThemSp themSp)
        {
            string? Email = HttpContext.Session.GetString("Email");
            if(Email == null) { return Json(new { success = false }); }
            await cartRespostory.ThemSanPhamVaoGioAsync(Email,themSp.MaSp, themSp.SoLuong);
            return Json(new { success = true});
        }
        [HttpPost]
        public async Task<IActionResult> CapNhatSP(string MaSp,int SoLuong)
        {
            string? Email = HttpContext.Session.GetString("Email");
            if (Email == null) { return Json(new { success = false }); }
            bool isvalid = await cartRespostory.CapNhatSoLuongSanPhamAsync(Email, MaSp, SoLuong);
            if (!isvalid)
            {
                return RedirectToAction("Err", "Home");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> XoaSp(string MaSp)
        {
            string? Email = HttpContext.Session.GetString("Email");
            if (Email == null) { return Json(new { success = false }); }
            await cartRespostory.XoaSanPhamKhoiGio(Email,MaSp);
            return RedirectToAction("Index");
        }
    
    }
}
