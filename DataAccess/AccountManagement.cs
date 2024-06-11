using FAP_BE.DTOs;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FAP_BE.DataAccess
{
    public class AccountManagement
    {
        private static FAP_PRN231Context _context;
        private static AccountManagement instance = null;
        private static readonly object _locker = new object();

        public static AccountManagement Instance
        {
            get
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new AccountManagement();
                        _context = new FAP_PRN231Context();
                    }
                    return instance;
                }
            }
        }

        public Account Login(LoginDTO loginDTO)
        {
            try
            {
                string username = loginDTO.Username;
                string password = loginDTO.Password;
                var account = _context.Accounts.Include(r => r.Role).FirstOrDefault(s => s.Username.Equals(username) && s.Password.Equals(password));
                return account;
            }catch(Exception ex)
            {
                return null;
            }
        }

        private const string Secretkey = "SuperSecretJwtKeyForFapProject12";
        private readonly TimeSpan TokenLifeSpan = TimeSpan.FromHours(3);

        public string GenerateJwtToken(AccountInfoDTO accountInfo)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(Secretkey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim("AccountId", accountInfo.AccountId.ToString()),
                    new Claim("Username", accountInfo.Username.ToString()),
                    new Claim(ClaimTypes.Role, accountInfo.Role.ToString())
                }),
                Expires = DateTime.UtcNow.Add(TokenLifeSpan),
                Issuer = "FPTUniversity",
                Audience = "FAPUser",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var jwt = jwtTokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
