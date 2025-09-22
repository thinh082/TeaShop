using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Respository
{
    public class PaymentRepository
    {
        private readonly MyDbContext _context;
        public PaymentRepository(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public LichSuThanhToan Payment_Order(string MaDonHang)
        {
            return _context.LichSuThanhToans.Where(r=>r.MaDonHang == MaDonHang).FirstOrDefault();
        }
    }
}
