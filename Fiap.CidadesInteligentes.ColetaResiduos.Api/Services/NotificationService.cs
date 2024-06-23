using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public void Add(NotificationModel model)
        {
            _notificationRepository.Add(model);
        }

        public void Delete(long id)
        {
            _notificationRepository.Delete(t => t.Id == id);
        }

        public IEnumerable<NotificationModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _notificationRepository.FindAll(page, pageSize);
        }

        public NotificationModel? FindById(long id)
        {
            return _notificationRepository.FindOneBy(t => t.Id == id);
        }

        public void Update(NotificationModel model)
        {
            _notificationRepository.Update(model);
        }

        public IEnumerable<NotificationModel> GetActiveNotifications(int page = 1, int pageSize = 10)
        {
            return _notificationRepository.GetActiveNotifications(page, pageSize);
        }
    }
}
