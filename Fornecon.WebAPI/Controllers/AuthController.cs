using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fornecon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _tokenService;

        public AuthController(IUserRepository user, ITokenService tokenService)
        {
            _user = user;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            var exists = await _user.GetUsersExists(user);

            if(exists != null)
            {
                var tokenString = _tokenService.GerarToken(user); 
                    
                return Ok(new { token = tokenString });
            }
            else
            {
                return BadRequest("Login Inválido!");
            }
        }

        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _user.GetUsers();

                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter os usuários" });
            }
        }
    }
}
