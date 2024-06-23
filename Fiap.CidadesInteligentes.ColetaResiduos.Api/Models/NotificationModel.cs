namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Models
{
    public class NotificationModel
    {
        public long Id { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsActive { get; set; }
    }
}
