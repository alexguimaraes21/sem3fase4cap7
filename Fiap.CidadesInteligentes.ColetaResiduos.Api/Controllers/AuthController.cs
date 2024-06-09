using Asp.Versioning;
using AutoMapper;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            var registeredUser = _userService.FindByEmail(registerViewModel.Email);
            if (registeredUser == null)
            {
                var user = _mapper.Map<UserModel>(registerViewModel);
                user.Role = "User";
                user.IsActive = true;
                _userService.Add(user);
                return Created();
            } else
            {
                return BadRequest(new { Error = "Usuário já cadastrado" });
            }
        }
    }
}
