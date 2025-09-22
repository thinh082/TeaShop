using Microsoft.AspNetCore.Mvc;
using ProjectTMDT.Services;
using TeaShop.Data;
using TeaShop.Models;
using TeaShop.Models.ParamModel;
using TeaShop.Responsitory;

namespace TeaShop.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly UserRespository _userRespository;
        private readonly EmailServices _emailServices;
        private readonly MyDbContext _context;
        public AuthenticateController(UserRespository userRespository, EmailServices emailServices,MyDbContext myDbContext)
        {
            _userRespository = userRespository;
            _emailServices = emailServices;
            _context = myDbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email,string MatKhau)
        {
            string mess = "";
            bool isvalid = _userRespository.DangNhap(Email, MatKhau,out TaiKhoan taiKhoan,out mess);
            if (isvalid)
            {
                if (taiKhoan.Role_id == true) 
                {
                    return Redirect("/Admin/Home/Index");
                }
                HttpContext.Session.SetString("Email", taiKhoan.Email);
                HttpContext.Session.SetString("Ten",taiKhoan.Ten);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Mess = mess;
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(TaiKhoan taiKhoan)
        {
            taiKhoan.IsEmail = false;
            _userRespository.DangKy(taiKhoan);
            return RedirectToAction("Login");
        }
        public IActionResult Email()
        {
            string Email = HttpContext.Session.GetString("Email");
            return View("Email",Email);
        }
        [HttpPost]
        public async Task<JsonResult> RequestOTP([FromBody] EModel model) 
        {
            var (success, error) = await _emailServices.SendMailAsync(model.Email);
            if (!success)
            {
                return Json(new { success = false, message = error });
            }
            return Json(new { success = true, message = "Thành công" });
        }
        [HttpPost]
        public JsonResult CheckOTP([FromBody] EModel model)
        {
            bool isvalid = _emailServices.KiemTraMaOTP(model.Email, model.Code);
            if (!isvalid) 
            {
                return Json(new { success = true, message = "Lỗi" });
            }
            var cs = _context.TaiKhoans.Where(r => r.Email == model.Email).FirstOrDefault();
            if (cs != null)
            {
                cs.IsEmail = true;
                _context.SaveChanges();
            }
            return Json(new { success = true, message = "mã OTP hợp lệ" });
        }
    }
}
