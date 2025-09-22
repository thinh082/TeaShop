using TeaShop.Data;
using TeaShop.Models;

namespace TeaShop.Respository
{
    public class ProductRepository
    {
        private readonly MyDbContext _context;
        public ProductRepository(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        public List<SanPham> GetProduct(string DanhMuc,string? TenSp)
        {
            return _context.SanPhams.Where(r=>r.MaDanhMuc == DanhMuc&&(TenSp==null|| r.TenSp.Contains(TenSp))).ToList();
        }
        public SanPham ProductDetail(string MaSp) 
        {
            return _context.SanPhams.Where(r=>r.MaSp == MaSp).FirstOrDefault(); 
        }
        public void Identity_Product(string MaSp)
        {
            var sp = _context.LuotXemSanPhams.FirstOrDefault(r=>r.MaSp == MaSp);
            sp.LuotXem += 1;
            _context.SaveChanges();
        }         
        public List<SanPham> Top4()
        {
            return _context.SanPhams.OrderByDescending(x=>x.DaBan).Take(4).ToList();
        }
    }
}
