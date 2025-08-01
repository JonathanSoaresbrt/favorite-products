using Favorite.Products.API.Constants;
using Favorite.Products.API.Dtos.Request;
using Favorite.Products.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Favorite.Products.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Login == ApiConstants.UserAdmLogin && request.Password == ApiConstants.UserAdmPassword)
            {
                var token = _tokenService.GenerateToken(request.Login, ApiConstants.TypeUserAdm);
                return Ok(new { token });
            }

            if (request.Login == ApiConstants.UserLogin && request.Password == ApiConstants.UserPassword)
            {
                var token = _tokenService.GenerateToken(request.Login, ApiConstants.TypeUser);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}