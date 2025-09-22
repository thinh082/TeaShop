 using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TeaShop.Models;
using TeaShop.Models.TableModel;
using TeaShop.Respository;

namespace TeaShop.Controllers
{
    public class DonHangController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly PaymentRepository _paymentRepostitory;
        public DonHangController(OrderRepository orderRepository, PaymentRepository paymentRepostitory)
        {
            _orderRepository = orderRepository;
            _paymentRepostitory = paymentRepostitory;
        }
        public IActionResult Index(int pageNumber )
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Home");
            }
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email },
        new SqlParameter("@PageNumber", SqlDbType.Int) { Value = pageNumber }
            };
            List<OrderDetail> pagedOrders = _orderRepository.ProcedureResult(sqlParameters);

            ViewBag.PageNumber = pageNumber;
            return View(pagedOrders);
        }

        public IActionResult Detail(string MaDonHang)
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Home");
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@MaDonHang", MaDonHang)
            };
            DataSet result = _orderRepository.ProcedureResult("sp_GetChiTietDonHang", parameters);
            if (result.Tables.Count > 0)
            {
                DataTable donHangTable = result.Tables[0];   // Thông tin đơn hàng
                DataTable chiTietTable = result.Tables[1];    // Chi tiết sản phẩm
                DataTable thanhToanTable = result.Tables[2];  // Lịch sử thanh toán
                ViewBag.donhang = donHangTable;
                ViewBag.chitiet = chiTietTable;
                ViewBag.thanhtoan = thanhToanTable;
            }
            return View();
           
        }
    }
}
