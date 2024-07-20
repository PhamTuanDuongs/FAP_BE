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

        public AccountInfoDTO Login(LoginDTO loginDTO)
        {
            try
            {
                string username = loginDTO.Email;
                string password = loginDTO.Password;
                AccountInfoDTO accountInfoDTO = new AccountInfoDTO();
                var account = _context.Accounts.Include(r => r.Role).Include(md => md.MetaData).FirstOrDefault(s => s.MetaData.Email.Equals(username) && s.Password.Equals(password));

                if (account == null) return null;

                if(account.Role.Name == "Student")
                {
                    var account2 = _context.Accounts.Include(r => r.Role).Include(md => md.MetaData).ThenInclude(s => s.Student).FirstOrDefault(s => s.MetaData.Email.Equals(username) && s.Password.Equals(password));
                    accountInfoDTO.Id = account2.MetaData.Student.Id;
                    accountInfoDTO.Username = account2.Username;
                    accountInfoDTO.AccountId = account2.AccountId;
                    accountInfoDTO.Role = "Student";
                }
                if(account.Role.Name == "Teacher")
                {
                    var account2 = _context.Accounts.Include(r => r.Role).Include(md => md.MetaData).ThenInclude(s => s.Instructor).FirstOrDefault(s => s.MetaData.Email.Equals(username) && s.Password.Equals(password));
                    accountInfoDTO.Id = account2.MetaData.Instructor.Id;
                    accountInfoDTO.Username = account2.Username;
                    accountInfoDTO.AccountId = account2.AccountId;
                    accountInfoDTO.Role = "Teacher";
                }
                if(account.Role.Name == "Admin")
                {
                    accountInfoDTO.Id = 0;
                    accountInfoDTO.Username = account.Username;
                    accountInfoDTO.AccountId = account.AccountId;
                    accountInfoDTO.Role = "Admin";
                }
                return accountInfoDTO;
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
                    new Claim("id", accountInfo.Id.ToString()),
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
