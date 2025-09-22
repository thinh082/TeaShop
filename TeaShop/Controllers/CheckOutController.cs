using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TeaShop.Models;
using TeaShop.Responsitory;
using TeaShop.Respository;

namespace TeaShop.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly UserRespository _userRespository;
        private readonly CartRespostory _cartRespostory;
        private readonly CheckOutRespository _checkOutRespository;
        public CheckOutController(UserRespository userRespository, CartRespostory cartRespostory,CheckOutRespository checkOutRespository)
        {
            _userRespository = userRespository;
            _cartRespostory = cartRespostory;
            _checkOutRespository= checkOutRespository;
        }
        public IActionResult Index()
        {
            string? Email = HttpContext.Session.GetString("Email");
            if (Email == null) { return RedirectToAction("Index", "Home"); }
            TaiKhoan tk = _userRespository.ThongTinNguoiDung(Email);
            decimal TongTien = _cartRespostory.ToTalPrice(Email);
            ViewBag.TongTien = TongTien;
            return View(tk);
        }
        [HttpPost]
        public async Task<IActionResult> Index(TaiKhoan taiKhoan,string HinhThucThanhToan)
        {
            string? Email = HttpContext.Session.GetString("Email");
            if (Email == null) { return RedirectToAction("Index", "Home"); }
            decimal TongTien = _cartRespostory.ToTalPrice(Email);
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Email",Email),
                new SqlParameter("@TongTien",TongTien),
                new SqlParameter("@HinhThucThanhToan",HinhThucThanhToan),
                new SqlParameter("@DiaChi",taiKhoan.DiaChi)
            };
            _checkOutRespository.ChuyenGioHangSangDonHang(sqlParameters);
            return RedirectToAction("Index", "Donhang", new {PageNumber =1});
        }
    }
}
