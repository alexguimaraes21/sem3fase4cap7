namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Models
{
    public class RouteModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long TruckId { get; set; }
        public TruckModel Truck { get; set; }
    }
}
