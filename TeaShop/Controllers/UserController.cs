using Microsoft.AspNetCore.Mvc;
using TeaShop.Models;
using TeaShop.Responsitory;

namespace TeaShop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRespository _userRespository;
        public UserController(UserRespository userRespository)
        {
            _userRespository = userRespository;
        }
        public IActionResult Inform()
        {
            TaiKhoan tk =  _userRespository.ThongTinNguoiDung(HttpContext.Session.GetString("Email"));
            return View(tk);
        }
        [HttpPost]
        public IActionResult UpdateInform(TaiKhoan taiKhoan)
        {
            _userRespository.CapNhatThongTinNguoiDung(taiKhoan);
            return RedirectToAction("Inform");
        }
    }
}
