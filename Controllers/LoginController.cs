using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private IMapper _mapper;

        public LoginController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken(LoginDTO loginDTO)
        {
            try
            {
                var account = _accountRepository.Login(loginDTO);
                if (account == null) return Ok("Login fail");
                var accountInfo = _mapper.Map<Account,AccountInfoDTO>(account);
                string token = _accountRepository.GenerateToken(accountInfo);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
