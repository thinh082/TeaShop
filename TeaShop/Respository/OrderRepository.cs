using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using TeaShop.Data;
using TeaShop.Models;
using TeaShop.Models.TableModel;

namespace TeaShop.Respository
{
    public class OrderRepository
    {
        private readonly MyDbContext _context;
        private readonly string _connectionstring;
        public OrderRepository(MyDbContext myDbContext, IConfiguration configuration)
        {
            _context = myDbContext;
            _connectionstring = configuration.GetConnectionString("MyConnectionString");
        }
        public DonHang OrderDetail(string MaDonHang, string Email)
        {
            var donhang = _context.DonHangs
                .Include(d => d.TaiKhoan)
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham) // lấy thông tin sản phẩm
                .FirstOrDefault(d => d.MaDonHang == MaDonHang && d.Email == Email);
            return donhang;
        }
        //public async Task<PagedResult<DonHang>> GetPagedOrdersAsync(string email, int pageNumber)
        //{
        //    var query = _context.DonHangs
        //        .Include(d => d.TaiKhoan)
        //        .Include(d => d.ChiTietDonHangs)
        //            .ThenInclude(ct => ct.SanPham)
        //        .Where(d => d.Email == email);

        //    var totalItems = await query.CountAsync();

        //    var orders = await query
        //        .OrderByDescending(d => d.MaDonHang) // ví dụ: đơn mới nhất lên trước
        //        .Skip((pageNumber - 1) * 10)
        //        .Take(10)
        //        .ToListAsync();

        //    return new PagedResult<DonHang>
        //    {
        //        Items = orders,
        //        TotalPages = (int)Math.Ceiling(totalItems / (double)10),
        //        PageNumber = pageNumber,
        //    };
        //}
        public List<OrderDetail> ProcedureResult(SqlParameter[] sqlParameters)
        {
            DataTable dataTable = new DataTable();
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand("GetPagedOrders", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }

            // Map DataTable to List<OrderDetail>
            foreach (DataRow row in dataTable.Rows)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    MaDonHang = row["MaDonHang"].ToString(),
                    Email = row["Email"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    NgayDat = Convert.ToDateTime(row["NgayDat"]),
                    NgayGiao = Convert.ToDateTime(row["NgayGiao"]),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    DiaChiGiaoHang = row["DiaChiGiaoHang"].ToString(),
                    MaSp = row["MaSp"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    Gia = Convert.ToDecimal(row["Gia"])
                };

                orderDetails.Add(orderDetail);
            }

            return orderDetails;
        }
        public DataSet ProcedureResult(string procedureName, SqlParameter[] sqlParameters)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(procedureName, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                    {
                        sqlDataAdapter.Fill(dataSet); // Fill nhiều bảng vào DataSet
                    }
                }
            }
            return dataSet;
        }


    }
}
