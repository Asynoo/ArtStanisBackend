using System;
using System.Security.Authentication;
using ArtStanisProject.Security.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtStanisProject_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login(LoginDto dto)
        {
            try
            {
                var token = _securityService.GenerateJwtToken(dto.Username, dto.Password);
                return Ok(new TokenDto
                {
                    Jwt = token.Jwt,
                    Message = token.Message
                });
            }
            catch (AuthenticationException ae)
            {
                return Unauthorized(ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Please contact admin");
            }
        }
    }

    public class TokenDto
    {
        public string Jwt { get; set; }
        public string Message { get; set; }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}