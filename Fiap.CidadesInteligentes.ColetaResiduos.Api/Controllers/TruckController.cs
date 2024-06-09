using Asp.Versioning;
using AutoMapper;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:ApiVersion}/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;
        private readonly IMapper _mapper;

        public TruckController(ITruckService truckService, IMapper mapper)
        {
            _truckService = truckService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public ActionResult<IEnumerable<PaginationResponseModel<TruckResponseModel>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var lista = _truckService.FindAll(page, pageSize);
            var responseModel = _mapper.Map<IEnumerable<TruckResponseModel>>(lista);
            var responseModelList = new PaginationResponseModel<TruckResponseModel>
            {
                List = responseModel,
                UrlBase = "Truck",
                CurrentPage = page,
                PageSize = pageSize
            };

            return Ok(responseModelList);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, User")]
        public ActionResult<TruckResponseModel> Get(long id)
        {
            var truck = _truckService.FindById(id);
            if (truck == null)
                return NotFound();
            return Ok(_mapper.Map<TruckResponseModel>(truck));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Add([FromBody] TruckViewModel viewModel)
        {
            var truck = _mapper.Map<TruckModel>(viewModel);
            _truckService.Add(truck);
            return CreatedAtAction(nameof(Get), new { id = truck.Id }, truck);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Update(long id, [FromBody] TruckViewModel viewModel) 
        {
            if (viewModel.Id == id)
            {
                var registeredTruck = _truckService.FindById(id);
                if (registeredTruck == null)
                    return NotFound();
                _mapper.Map(viewModel, registeredTruck);
                _truckService.Update(registeredTruck);
                return NoContent();
            }
            else 
            {
                return BadRequest(new { Error = "O ID informado na URL não é o mesmo do corpo da requisição" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(long id)
        {
            _truckService.Delete(id);
            return NoContent();
        }
    }
}
