using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;

namespace FAP_BE.Service
{
    public class AccountRepository : IAccountRepository
    {
        public string GenerateToken(AccountInfoDTO accountInfo) => AccountManagement.Instance.GenerateJwtToken(accountInfo);

        public Account Login(LoginDTO loginDTO) => AccountManagement.Instance.Login(loginDTO);

    }
}
