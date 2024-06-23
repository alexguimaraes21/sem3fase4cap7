using Asp.Versioning;
using AutoMapper;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = _userService.FindAll(page, pageSize);
            var responseModelList = _mapper.Map<IEnumerable<UserResponseModel>>(users);

            var responseModel = new PaginationResponseModel<UserResponseModel>
            {
                UrlBase = "User",
                List = responseModelList,
                PageSize = pageSize,
                CurrentPage = page,
            };
            return Ok(responseModel);
        }

        [HttpGet("FindByEmail")]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult FindByEmail([FromQuery] string email)
        {
            var user = _userService.FindByEmail(email);
            if(user == null) 
                return NotFound();
            return Ok(_mapper.Map<UserResponseModel>(user));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Get(long id)
        {
            var user = _userService.FindById(id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserResponseModel>(user));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] UserViewModel viewModel)
        {
            var userExistente = _userService.FindById(id);
            if (userExistente == null) 
                return NotFound();

            if (userExistente.Id != id)
                return BadRequest();

            _mapper.Map(viewModel, userExistente);
            _userService.Update(userExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var userExistente = _userService.FindById(id);
            if (userExistente == null)
                return NotFound();

            if (userExistente.Id != id)
                return BadRequest();

            _userService.Delete(userExistente.Id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] UserViewModel viewModel)
        {
            var userEncontrado = _userService.FindByEmail(viewModel.Email);
            if (userEncontrado == null)
            {
                var user = _mapper.Map<UserModel>(viewModel);
                _userService.Add(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            else
            {
                throw new Exception("Usuário já existe");
            }
        }
    }
}
