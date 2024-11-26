using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAWPMD.Architecture.Authentication;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Models.Models;
using PAWPMD.Service.Services;

namespace PAWPMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AccountController(IAccountService accountService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _accountService = accountService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            try
            {
                var user = await _accountService.RegisterAsync(registerRequest);
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var loginResponse = await _accountService.LoginAsync(loginRequest);

                var token = _jwtTokenGenerator.Generate(loginResponse);

                var userDto = new UserDTO
                {
                    Email = loginResponse.User.Email,
                    LastName = loginResponse.User.LastName,
                    Name = loginResponse.User.Name,
                    Token = token
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    
    }
}