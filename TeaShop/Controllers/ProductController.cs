using Microsoft.AspNetCore.Mvc;
using TeaShop.Models;
using TeaShop.Respository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TeaShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _product;
        public ProductController(ProductRepository product)
        {
            _product = product;
        }
        public IActionResult Index(string query,string? TenSp)
        {
            ViewBag.query = query;
            List<SanPham> sp = new List<SanPham>();
            switch (query)
            {
                case "ThaoMoc":
                    sp= _product.GetProduct("MaDanhMuc1",TenSp);
                    break;
                case "TruyenThong":
                    sp= _product.GetProduct("MaDanhMuc2",TenSp);
                    break;
                case "UopHuong":
                    sp = _product.GetProduct("MaDanhMuc3", TenSp);
                    break;
                case "DacBiet":
                    sp = _product.GetProduct("MaDanhMuc4", TenSp);
                    break;
            }
            return View(sp);
        }
        public IActionResult Details(string MaSp) 
        {
            ViewBag.Top4 = _product.Top4();
            SanPham sp = _product.ProductDetail(MaSp);
            _product.Identity_Product(MaSp);
            return View(sp);
        }
    }
}
