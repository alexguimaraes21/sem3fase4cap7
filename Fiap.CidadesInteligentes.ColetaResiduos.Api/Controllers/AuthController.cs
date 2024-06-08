using Asp.Versioning;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Libs;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginViewModel user)
        {
            var authenticatedUser = _authService.Authenticate(user.Email, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            var token = Util.GenerateJwtToken(authenticatedUser);
            return Ok(new { Token = token });
        }
    }
}
