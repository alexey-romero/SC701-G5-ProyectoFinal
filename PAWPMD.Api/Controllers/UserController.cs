using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAWPMD.Architecture.Authentication;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Models;
using PAWPMD.Models.Models;
using PAWPMD.Service.Services;

namespace PAWPMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
      

        public UserController(IUserService userService)
        {
             _userService = userService;
        }

       [HttpGet("{id}", Name = "GetUser")]
       public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var updatedUser = await _userService.SaveUser(user);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

    }
}
