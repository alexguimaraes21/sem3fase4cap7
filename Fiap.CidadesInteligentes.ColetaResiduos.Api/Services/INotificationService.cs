using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface INotificationService : IGenericService<NotificationModel>
    {
        IEnumerable<NotificationModel> GetActiveNotifications(int page = 1, int pageSize = 10);
    }
}
