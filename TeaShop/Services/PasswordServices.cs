using Microsoft.AspNetCore.Identity;

namespace TeaShop.Services
{
    public class PasswordServices
    {
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
        public string HashPass(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }
        public PasswordVerificationResult HashPasswordVerification(string hashpassword, string password)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashpassword, password);
        }
    }
}
