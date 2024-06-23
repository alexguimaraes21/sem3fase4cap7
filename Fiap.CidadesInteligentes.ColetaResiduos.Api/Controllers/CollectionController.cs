using Asp.Versioning;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:ApiVersion}/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet("FindAllScheduledCollections")]
        [Authorize(Roles = "Admin, Manager, User")]
        public IActionResult FindAllScheduledCollections([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var collections = _collectionService.FindAllScheduledCollections(page, pageSize);
            if (collections == null)
                return NotFound();

            var responseModelList = new PaginationResponseModel<CollectionModel>
            {
                List = collections,
                UrlBase = "Collection",
                CurrentPage = page,
                PageSize = pageSize
            };

            return Ok(responseModelList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public IActionResult findAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var collections = _collectionService.FindAll(page, pageSize);
            if (collections == null)
                return NotFound();

            var responseModelList = new PaginationResponseModel<CollectionModel>
            {
                List = collections,
                UrlBase = "Collection",
                CurrentPage = page,
                PageSize = pageSize
            };

            return Ok(responseModelList);
        }

        [HttpPatch("FinalizeCollection/{id}")]
        [Authorize(Roles = "Admin, Manager, User")]
        public IActionResult FinalizeCollection(long id)
        {
            var collection = _collectionService.FindById(id);
            if(collection == null)
                return NotFound();
            _collectionService.FinalizeCollection(id);
            return NoContent();
        }
    }
}
