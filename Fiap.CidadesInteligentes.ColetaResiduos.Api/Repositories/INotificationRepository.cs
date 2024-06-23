using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public interface INotificationRepository : IGenericRepository<NotificationModel>
    {
        IEnumerable<NotificationModel> GetActiveNotifications(int page = 1, int pageSize = 10);
    }
}
