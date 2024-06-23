using Asp.Versioning;
using AutoMapper;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:ApiVersion}/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService,
            IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActiveNotifications([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var notifications = _notificationService.GetActiveNotifications(page, pageSize);
            if (notifications == null)
                return NotFound();

            var responseModelList = new PaginationResponseModel<NotificationModel>
            {
                List = notifications,
                UrlBase = "Notification",
                CurrentPage = page,
                PageSize = pageSize
            };
            return Ok(responseModelList);
        }
    }
}
