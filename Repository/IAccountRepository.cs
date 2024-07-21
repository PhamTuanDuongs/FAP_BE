using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface IAccountRepository
    {
        public AccountInfoDTO Login(LoginDTO loginDTO);

        public string GenerateToken(AccountInfoDTO accountInfo);
    }
}
