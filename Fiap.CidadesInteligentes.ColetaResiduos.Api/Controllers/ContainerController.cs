using Asp.Versioning;
using AutoMapper;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:ApiVersion}/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerService _containerService;
        private readonly IMapper _mapper;

        public ContainerController(IContainerService containerService, IMapper mapper)
        {
            _containerService = containerService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public ActionResult<IEnumerable<PaginationResponseModel<ContainerResponseModel>>> Get([FromQuery] int page = 1, int pageSize = 10)
        {
            var list = _containerService.FindAll(page, pageSize);
            var responseModel = _mapper.Map<IEnumerable<ContainerResponseModel>>(list);
            var responseModelList = new PaginationResponseModel<ContainerResponseModel>
            {
                List = responseModel,
                UrlBase = "Container",
                CurrentPage = page,
                PageSize = pageSize
            };

            return Ok(responseModelList);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, User")]
        public ActionResult<ContainerResponseModel> Get(long id)
        {
            var container = _containerService.FindById(id);
            if (container == null)
                return NotFound();
            return Ok(_mapper.Map<ContainerResponseModel>(container));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Add([FromBody] ContainerViewModel viewModel)
        {
            var container = _mapper.Map<ContainerModel>(viewModel);
            _containerService.Add(container);
            return CreatedAtAction(nameof(Get), new { id = container.Id }, container);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Update(long id, [FromBody] ContainerViewModel viewModel)
        {
            if (viewModel.Id == id)
            {
                var registeredContainer = _containerService.FindById(id);
                if (registeredContainer == null)
                    return NotFound();
                _mapper.Map(viewModel, registeredContainer);
                _containerService.Update(registeredContainer);
                return NoContent();
            }
            else {
                return BadRequest(new { Error = "O Id informado na URL não é o mesmo do corpo da requisição"});
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(long id) 
        {
            _containerService.Delete(id);
            return NoContent();
        }
    }
}
