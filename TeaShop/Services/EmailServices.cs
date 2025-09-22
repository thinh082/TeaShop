using System.Net.Mail;
using System.Net;
using Microsoft.Data.SqlClient;
using System.Data;
using TeaShop.Data;
using TeaShop.Models;

namespace ProjectTMDT.Services
{
    public class EmailServices
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;
        public EmailServices(IConfiguration configuration,MyDbContext myDbContext)
        {
            _configuration = configuration;
            _context = myDbContext;
        }
        public async Task<(bool Success, string ErrorMessage)> SendMailAsync(string receptor)
        {
            try
            {
                var email = _configuration.GetValue<string>("EmailSetting:SmtpUsername");
                var password = _configuration.GetValue<string>("EmailSetting:SmtpPassword");
                var host = _configuration.GetValue<string>("EmailSetting:SmtpServer");
                var port = _configuration.GetValue<int>("EmailSetting:SmtpPort");

                using var smtpClient = new SmtpClient(host, port)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password)
                };

                var code = Guid.NewGuid().ToString().Substring(0, 8);
                bool isvalid = KiemTraMaOTP(receptor, code);
                if (isvalid)
                {
                    CapNhatMaOTP(receptor, code);
                }
                var message = new MailMessage(email!, receptor, "Xác nhận mã OTP", $"Mã xác thực của bạn là: {code}");
                await smtpClient.SendMailAsync(message);
                ThemOTP(receptor, code);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public bool KiemTraMaOTP(string receptor,string code)
        {
          var rs =  _context.TaiKhoans.Where(r=>r.Email == receptor && r.Code == code).FirstOrDefault();
            if (rs == null) { return false; }
            return true;        
        }   
        public void CapNhatMaOTP(string receptor,string code)
        {
            var rs =  _context.TaiKhoans.Where(r=>r.Email == receptor).FirstOrDefault();
            rs.Code = code;
            _context.SaveChanges();
        }
        public void ThemOTP(string receptor,string code)
        {
            var rs = _context.TaiKhoans.Where(r => r.Email == receptor).FirstOrDefault();
            rs.Code = code;
            _context.SaveChanges();
        }
        public void XoaOTP (string receptor,string code)
        {
            var rs = _context.TaiKhoans.Where(r => r.Email == receptor).FirstOrDefault();
            rs.Code = null;
            _context.SaveChanges();
        }
    }
}
