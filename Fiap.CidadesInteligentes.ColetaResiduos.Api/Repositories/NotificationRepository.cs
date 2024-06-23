using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class NotificationRepository : GenericRepository<NotificationModel>, INotificationRepository
    {
        public NotificationRepository(DatabaseContext dbContext) : base(dbContext) { }
        
        public IEnumerable<NotificationModel> GetActiveNotifications(int page = 1, int pageSize = 10)
        {
            return _dbContext.Set<NotificationModel>()
                .Where(t => t.ValidUntil >= DateTime.Now)
                .Skip((page - 1) * page)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();
        }
    }
}
